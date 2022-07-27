using Service.Clients;

namespace API.Services;

public class HourlyWeatherBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromHours(1));
    private readonly IWeatherClient _weather;

    public HourlyWeatherBackgroundService(IWeatherClient weather)
    {
        _weather = weather;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
        {
            var weather = await _weather.GetWeatherAsync("chicago");

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(weather));

            await Task.Delay(1000, stoppingToken);
        }
    }
}
