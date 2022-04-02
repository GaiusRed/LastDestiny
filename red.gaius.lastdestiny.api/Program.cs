using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Red.Gaius.LastDestiny.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",  new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Last Destiny API",
        Version = "v1",
        Description = "Fanmade Digital Card Game for Final Fantasy",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "erik@gaius.red",
            Name = "Erik Gaius Capistrano",
        },
    });
});

var options = new ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights"),
};

builder.Services.AddApplicationInsightsTelemetry(options: options);

builder.Host.ConfigureLogging((context, builder) =>
 {
     builder.AddApplicationInsights();

     // Capture all log-level entries from Program
     builder.AddFilter<ApplicationInsightsLoggerProvider>(
        typeof(Program).FullName, LogLevel.Trace);
});

builder.Services.AddSingleton<Database>();

// Build and run application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    TelemetryDebugWriter.IsTracingDisabled = true;
}

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "api";
    c.SwaggerEndpoint("v1/swagger.json", "Last Destiny API v1");
});

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
