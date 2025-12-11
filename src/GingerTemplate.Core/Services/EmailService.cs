using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public void SendEmail(string to, string subject, string body)
    {
        // Simulate sending an email
        _logger.LogInformation("Sending email to {To} with subject {Subject}.", to, subject);
        // Actual email sending logic would go here
    }
}