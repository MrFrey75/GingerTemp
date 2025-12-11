using System.Collections.Generic;
using GingerTemplate.Core.Configuration;
using GingerTemplate.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, services, loggerConfig) =>
    {
        loggerConfig
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "GingerTemplate.CLI")
            .WriteTo.Console();
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<ILoggingService, LoggingService>();
        
        var configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        services.AddSingleton<IConfigurationService>(new ConfigurationService(configPath));
        services.AddSingleton(new ConfigurationFileManager(Path.Combine(AppContext.BaseDirectory, "config")));
    })
    .Build();

var logger = host.Services.GetRequiredService<ILoggingService>();
var configService = host.Services.GetRequiredService<IConfigurationService>();

using (logger.BeginCorrelationScope())
{
    logger.LogInformation("CLI started");
    logger.LogInformation("Arguments received", new Dictionary<string, object?> { ["Args"] = args });
    
    // Example: Load configuration
    try
    {
        var appSettings = configService.GetSection<ApplicationSettings>("Application");
        logger.LogInformation($"App: {appSettings.AppName} v{appSettings.AppVersion}");
    }
    catch (Exception ex)
    {
        logger.LogError($"Failed to load application configuration: {ex.Message}", ex);
    }
}

await host.StopAsync();