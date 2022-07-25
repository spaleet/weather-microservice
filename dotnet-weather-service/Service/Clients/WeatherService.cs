using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Service.Infrastructure;
using Service.Models;

namespace Service.Clients;

public class WeatherClient : IWeatherClient
{
    private readonly WeatherSettings _settings;
    private readonly HttpClient _client;

    public WeatherClient(IHttpClientFactory factory, IOptions<WeatherSettings> weatherSettings)
    {
        _client = factory.CreateClient("weather");
        _settings = weatherSettings.Value;
    }

    public async Task<Weather> GetWeatherAsync(string city, string? unit)
    {
        var query = new Dictionary<string, string>()
        {
            ["appid"] = _settings.ApiKey,
            ["city"] = city,
            ["lang"] = "en",
            ["unit"] = unit ?? "metric",
        };

        string uri = QueryHelpers.AddQueryString("/weather", query);

        return await _client.GetFromJsonAsync<Weather>(uri);
    }
}

