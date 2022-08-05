namespace Service.Interfaces;

public interface IWeatherClient
{
    Task<Models.Weather> GetWeatherAsync(string city, string unit = "metric");
}
