using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Service.Implementations;

public class WeatherClient : Interfaces.IWeatherClient
{
    private readonly WeatherSettings _settings;
    private readonly HttpClient _client;

    public WeatherClient(IOptions<WeatherSettings> weatherSettings, IHttpClientFactory factory)
    {
        _settings = weatherSettings.Value;
        _client = factory.CreateClient("weather");
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
        res.EnsureSuccessStatusCode();

        string content = await res.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Weather>(content);
    }
}

