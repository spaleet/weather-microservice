namespace API.Services;

public class HourlyWeatherBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("BackgroundService is working fine !");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
