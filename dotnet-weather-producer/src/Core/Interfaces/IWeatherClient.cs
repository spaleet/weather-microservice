namespace Core.Interfaces;

public interface IWeatherClient
{
    Task<Weather> GetWeatherAsync(string city, string unit = "metric");
}