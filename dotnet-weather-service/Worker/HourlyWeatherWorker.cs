﻿using Service.Interfaces;

namespace Worker;

public class HourlyWeatherWorker : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(15));
    private readonly IWeatherClient _weather;
    private readonly IKafkaService _messager;

    public HourlyWeatherWorker(IWeatherClient weather, IKafkaService messager)
    {
        _weather = weather;
        _messager = messager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
        {
            string city = "chicago";

            var weather = await _weather.GetWeatherAsync(city);

            await _messager.ProduceAsync(city, weather);
        }
    }
}