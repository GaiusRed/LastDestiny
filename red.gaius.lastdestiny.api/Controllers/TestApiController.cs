using Microsoft.AspNetCore.Mvc;
using Red.Gaius.LastDestiny.Api.Models;

namespace Red.Gaius.LastDestiny.Api.Controllers;

/// <summary>
/// Example API controller.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TestApiController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    };

    private readonly ILogger<TestApiController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestApiController"/> class.
    /// </summary>
    /// <param name="logger">Logging service.</param>
    public TestApiController(ILogger<TestApiController> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Sample Get Method.
    /// </summary>
    /// <returns>The Weather Forecast.</returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        this.logger.LogInformation("Hello, world!");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        })
        .ToArray();
    }
}
