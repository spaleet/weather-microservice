using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Service.Infrastructure;
using Service.Models;

namespace Service.Clients;

public class WeatherClient : IWeatherClient
{
    private readonly WeatherSettings _settings;
    private readonly HttpClient _client;

    public WeatherClient(IHttpClientFactory factory, WeatherSettings weatherSettings)
    {
        _client = factory.CreateClient("weather");
        _settings = weatherSettings;
    }

    public async Task<Weather> GetWeatherAsync(string city, string unit = "metric")
    {
        var query = new Dictionary<string, string>()
        {
            ["q"] = city,
            ["appid"] = _settings.ApiKey,
            ["lang"] = "en",
            ["unit"] = unit,
        };

        string uri = QueryHelpers.AddQueryString("/data/2.5/weather", query);

        var res = await _client.GetAsync(uri);
        string content = await res.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Weather>(content);
    }
}

