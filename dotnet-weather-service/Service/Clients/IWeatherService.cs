namespace Service.Clients;

public interface IWeatherClient
{
    Task<Models.Weather> GetWeatherAsync(string city, string? unit);
}
