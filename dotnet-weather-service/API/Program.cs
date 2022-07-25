using API.Services;
using Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WeatherSettings>(builder.Configuration.GetSection("WeatherSettings"));

builder.Services.AddHostedService<HourlyWeatherBackgroundService>();

var app = builder.Build();

app.Run();
