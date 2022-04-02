using LiteDB;
using Red.Gaius.LastDestiny.Api.Models;

namespace Red.Gaius.LastDestiny.Services;

/// <summary>
/// Persistence service using LiteDB.
/// </summary>
public class Database
{
    private readonly ILogger<Database> logger;
    private readonly LiteDatabase liteDb;

    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    /// <param name="logger">Logging service.</param>
    /// <param name="config">Host configuration to retrieve database connection string.</param>
    public Database(ILogger<Database> logger, IConfiguration config)
    {
        this.logger = logger;
        this.liteDb = new LiteDatabase(config.GetConnectionString("LiteDB"));

        this.logger.LogInformation("Database initialized.");
    }

    /// <summary>
    /// Tests data write.
    /// </summary>
    /// <param name="weatherForecast">Data to persist.</param>
    public void TestWrite(WeatherForecast weatherForecast)
    {
        this.logger.LogInformation("Testing data write.");
        try
        {
            var col = this.liteDb.GetCollection<WeatherForecast>("TestCollection");
            col.DeleteAll();
            col.Insert(weatherForecast);
            col.EnsureIndex(i => i.Date);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Test write failed.");
        }
    }

    /// <summary>
    /// Tests data read.
    /// </summary>
    /// <returns>Data that was persisted by TestWrite.</returns>
    public WeatherForecast TestRead()
    {
        this.logger.LogInformation("Testing data read.");
        try
        {
            var col = this.liteDb.GetCollection<WeatherForecast>("TestCollection");
            return col.FindOne(i => i.Summary!.Length > 0);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Test read failed.");
            throw;
        }
    }
}