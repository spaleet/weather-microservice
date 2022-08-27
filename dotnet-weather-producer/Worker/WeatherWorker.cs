using Core.Interfaces;

namespace Worker;

public class WeatherWorker : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(5));
    private readonly IWeatherClient _weather;
    private readonly IPublisher _messager;

    public WeatherWorker(IWeatherClient weather, IPublisher messager)
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
