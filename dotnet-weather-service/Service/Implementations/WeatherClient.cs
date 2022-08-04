using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Service.Infrastructure;
using Service.Models;

namespace Service.Implementations;

public class WeatherClient : Intefaces.IWeatherClient
{
    private readonly WeatherSettings _settings;
    private readonly HttpClient _client;

    public WeatherClient(IOptions<WeatherSettings> weatherSettings)
    {
        _settings = weatherSettings.Value;
        _client = new HttpClient
        {
            BaseAddress = new Uri(_settings.BaseUrl)
        };
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

        string uri = QueryHelpers.AddQueryString("weather", query);

        var res = await _client.GetAsync(uri);
        string content = await res.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Weather>(content);
    }
}

