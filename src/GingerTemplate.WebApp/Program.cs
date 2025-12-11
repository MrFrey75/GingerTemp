using GingerTemplate.Core.Configuration;
using GingerTemplate.Core.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "GingerTemplate.WebApp")
        .WriteTo.Console();
});

builder.Services.AddSingleton<ILoggingService, LoggingService>();
builder.Services.AddRazorPages();

var configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
builder.Services.AddSingleton<IConfigurationService>(new ConfigurationService(configPath));
builder.Services.AddSingleton(new ConfigurationFileManager(Path.Combine(AppContext.BaseDirectory, "config")));

var app = builder.Build();

app.UseSerilogRequestLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTimeOffset.UtcNow }))
   .WithName("HealthCheck");

app.Run();
