using API.Services;
using Service.Clients;
using Service.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        var weatherSettings = config.GetSection("WeatherSettings").Get<WeatherSettings>();

        services.AddSingleton(weatherSettings);
        services.AddHttpClient("weather", opt =>
        {
            opt.BaseAddress = new Uri(weatherSettings.BaseUrl);
        });

        services.Configure<KafkaSettings>(config.GetSection("KafkaSettings"));

        services.AddTransient<IWeatherClient, WeatherClient>();
        services.AddHostedService<HourlyWeatherBackgroundService>();

        return services;
    }
}