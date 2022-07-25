using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<HourlyWeatherBackgroundService>();

var app = builder.Build();

app.Run();
