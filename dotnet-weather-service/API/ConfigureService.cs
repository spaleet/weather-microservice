﻿using API.Services;
using Service.Implementations;
using Service.Infrastructure;
using Service.Intefaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<WeatherSettings>(config.GetSection("WeatherSettings"));
        services.Configure<KafkaSettings>(config.GetSection("KafkaSettings"));

        services.AddHttpClient("weather", opt =>
        {
            opt.BaseAddress = new Uri(config["WeatherSettings:BaseUrl"]);
        });

        services.AddTransient<IWeatherClient, WeatherClient>();
        services.AddTransient<IKafkaService, KafkaService>();
        services.AddHostedService<HourlyWeatherBackgroundService>();

        return services;
    }
}