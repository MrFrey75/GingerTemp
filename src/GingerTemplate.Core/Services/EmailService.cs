using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Interface for email service operations.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;IEmailService&gt;(sp => new EmailService(sp.GetRequiredService&lt;ILogger&lt;EmailService&gt;&gt;(), smtpServer:"smtp.gmail.com", smtpPort:587, senderEmail:"noreply@gingertemplate.com", senderPassword:"secret", enableSsl:true));
/// var email = sp.GetRequiredService&lt;IEmailService&gt;();
/// await email.SendEmailAsync("user@example.com", "Welcome", "Hello!", isHtml:true);
/// </code>
/// </remarks>
public interface IEmailService
{
    /// <summary>
    /// Sends a simple email message
    /// </summary>
    /// <param name="to">Recipient email address</param>
    /// <param name="subject">Email subject</param>
    /// <param name="body">Email body content</param>
    /// <param name="isHtml">Whether the body is HTML formatted</param>
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);

    /// <summary>
    /// Sends an email to multiple recipients
    /// </summary>
    /// <param name="recipients">List of recipient email addresses</param>
    /// <param name="subject">Email subject</param>
    /// <param name="body">Email body content</param>
    /// <param name="isHtml">Whether the body is HTML formatted</param>
    Task SendEmailToMultipleAsync(IEnumerable<string> recipients, string subject, string body, bool isHtml = false);

    /// <summary>
    /// Sends an email with attachments
    /// </summary>
    /// <param name="to">Recipient email address</param>
    /// <param name="subject">Email subject</param>
    /// <param name="body">Email body content</param>
    /// <param name="attachmentPaths">List of file paths to attach</param>
    /// <param name="isHtml">Whether the body is HTML formatted</param>
    Task SendEmailWithAttachmentsAsync(string to, string subject, string body, IEnumerable<string> attachmentPaths, bool isHtml = false);

    /// <summary>
    /// Sends an email with custom sender information
    /// </summary>
    /// <param name="from">Sender email address</param>
    /// <param name="to">Recipient email address</param>
    /// <param name="subject">Email subject</param>
    /// <param name="body">Email body content</param>
    /// <param name="isHtml">Whether the body is HTML formatted</param>
    Task SendEmailFromAsync(string from, string to, string subject, string body, bool isHtml = false);
}

/// <summary>
/// Email service implementation using SMTP
/// </summary>
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _senderPassword;
    private readonly bool _enableSsl;

    public EmailService(ILogger<EmailService> logger, string smtpServer = "smtp.gmail.com", int smtpPort = 587, 
        string senderEmail = "noreply@gingertemplate.com", string senderPassword = "", bool enableSsl = true)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation("EmailService initialized.");
        _smtpServer = smtpServer ?? throw new ArgumentNullException(nameof(smtpServer));
        _smtpPort = smtpPort;
        _senderEmail = senderEmail ?? throw new ArgumentNullException(nameof(senderEmail));
        _senderPassword = senderPassword ?? "";
        _enableSsl = enableSsl;
    }

    /// <inheritdoc/>
    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                _logger.LogWarning("Cannot send email: recipient address is empty");
                return;
            }

            _logger.LogInformation("Attempting to send email to {To} with subject: {Subject}", to, subject);

            using (var mailMessage = new MailMessage(_senderEmail, to))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                using (var smtpClient = CreateSmtpClient())
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email successfully sent to {To}", to);
                }
            }
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP error while sending email to {To}", to);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending email to {To}", to);
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task SendEmailToMultipleAsync(IEnumerable<string> recipients, string subject, string body, bool isHtml = false)
    {
        if (recipients == null)
        {
            _logger.LogWarning("Cannot send email: recipients list is null");
            return;
        }

        var recipientList = new List<string>(recipients);
        if (recipientList.Count == 0)
        {
            _logger.LogWarning("Cannot send email: recipients list is empty");
            return;
        }

        try
        {
            _logger.LogInformation("Attempting to send email to {RecipientCount} recipients with subject: {Subject}", 
                recipientList.Count, subject);

            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_senderEmail);
                foreach (var recipient in recipientList)
                {
                    if (!string.IsNullOrWhiteSpace(recipient))
                    {
                        mailMessage.To.Add(recipient);
                    }
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                using (var smtpClient = CreateSmtpClient())
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email successfully sent to {RecipientCount} recipients", recipientList.Count);
                }
            }
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP error while sending email to multiple recipients");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending email to multiple recipients");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task SendEmailWithAttachmentsAsync(string to, string subject, string body, 
        IEnumerable<string> attachmentPaths, bool isHtml = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                _logger.LogWarning("Cannot send email: recipient address is empty");
                return;
            }

            if (attachmentPaths == null)
            {
                _logger.LogWarning("Attachment paths are null, sending email without attachments");
                await SendEmailAsync(to, subject, body, isHtml);
                return;
            }

            _logger.LogInformation("Attempting to send email with attachments to {To}", to);

            using (var mailMessage = new MailMessage(_senderEmail, to))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                foreach (var attachmentPath in attachmentPaths)
                {
                    if (!string.IsNullOrWhiteSpace(attachmentPath))
                    {
                        try
                        {
                            mailMessage.Attachments.Add(new Attachment(attachmentPath));
                            _logger.LogInformation("Attachment added: {AttachmentPath}", attachmentPath);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to attach file: {AttachmentPath}", attachmentPath);
                        }
                    }
                }

                using (var smtpClient = CreateSmtpClient())
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email with attachments successfully sent to {To}", to);
                }
            }
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP error while sending email with attachments to {To}", to);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending email with attachments to {To}", to);
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task SendEmailFromAsync(string from, string to, string subject, string body, bool isHtml = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(from))
            {
                _logger.LogWarning("Cannot send email: sender address is empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                _logger.LogWarning("Cannot send email: recipient address is empty");
                return;
            }

            _logger.LogInformation("Attempting to send email from {From} to {To}", from, to);

            using (var mailMessage = new MailMessage(from, to))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;

                using (var smtpClient = CreateSmtpClient())
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email successfully sent from {From} to {To}", from, to);
                }
            }
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP error while sending email from {From} to {To}", from, to);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending email from {From} to {To}", from, to);
            throw;
        }
    }

    /// <summary>
    /// Creates and configures an SMTP client
    /// </summary>
    private SmtpClient CreateSmtpClient()
    {
        var smtpClient = new SmtpClient
        {
            Host = _smtpServer,
            Port = _smtpPort,
            EnableSsl = _enableSsl,
            Timeout = 10000
        };

        if (!string.IsNullOrEmpty(_senderPassword))
        {
            smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
        }

        _logger.LogDebug("SMTP client configured: Server={Server}, Port={Port}, SSL={EnableSSL}", 
            _smtpServer, _smtpPort, _enableSsl);

        return smtpClient;
    }
}