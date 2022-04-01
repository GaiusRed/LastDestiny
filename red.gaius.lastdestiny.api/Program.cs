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

var app = builder.Build();

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
