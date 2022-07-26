﻿using Core.Settings;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using Worker;
using Core.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Messaging;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistry
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<WeatherSettings>(config.GetSection("WeatherSettings"));
        services.Configure<KafkaSettings>(config.GetSection("KafkaSettings"));

        services.AddHttpClient<IWeatherClient, WeatherClient>(opt =>
        {
            opt.BaseAddress = new Uri(config["WeatherSettings:BaseUrl"]);
        }).AddPolicyHandler(GetRetryPolicy(3));

        services.AddTransient<IPublisher, KafkaPublisherService>();
        services.AddHostedService<WeatherWorker>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int maxRetries)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, times => TimeSpan.FromSeconds(times),
                onRetry: (message, sleepDuration, attemptNumber, context) =>
                {
                    Log.Error("ERROR : {message}. Retrying in {sleepDuration}. {attemptNumber} / {max_retries}",
                              message.Result.StatusCode,
                              sleepDuration,
                              attemptNumber,
                              maxRetries);
                });
    }

}