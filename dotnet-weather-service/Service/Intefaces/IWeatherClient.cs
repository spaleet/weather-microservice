namespace Service.Intefaces;

public interface IWeatherClient
{
    Task<Models.Weather> GetWeatherAsync(string city, string unit = "metric");
}
