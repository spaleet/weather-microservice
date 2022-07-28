using Service.Clients;

namespace API.Services;

public class HourlyWeatherBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(1));
    private readonly IWeatherClient _weather;
    private readonly ILogger<HourlyWeatherBackgroundService> _logger;

    public HourlyWeatherBackgroundService(IWeatherClient weather, ILogger<HourlyWeatherBackgroundService> logger)
    {
        _weather = weather;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
        {
            string city = "chicago";

            var weather = await _weather.GetWeatherAsync(city);

            _logger.LogInformation("The temperature in {city}, {country} is currently {temperature} °C", city, weather.SystemWeather.Country, weather.Main.CelsiusCurrent);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
