using API.Services;
using Service.Implementations;
using Service.Infrastructure;
using Service.Intefaces;
using Polly;
using Polly.Extensions.Http;
using Serilog;

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
        }).AddPolicyHandler(GetRetryPolicy());

        services.AddTransient<IWeatherClient, WeatherClient>();
        services.AddTransient<IKafkaService, KafkaService>();
        services.AddHostedService<HourlyWeatherBackgroundService>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, times => TimeSpan.FromSeconds(times),
                onRetry: (message, sleepDuration, attemptNumber, context) =>
                {
                    Log.Error("ERROR : {message}. Retrying in {sleepDuration}. {attemptNumber} / {max_retries}", message.Result.StatusCode, sleepDuration, attemptNumber, 3);
                });
    }

}