namespace API.Services;

public class HourlyWeatherBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(5));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("BackgroundService is working fine !");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
