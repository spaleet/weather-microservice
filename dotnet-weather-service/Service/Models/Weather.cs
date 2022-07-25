namespace Service.Models;

public class Weather : BaseWeather
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("visibility")]
    public double Visibility { get; set; }

    [JsonPropertyName("sys")]
    public SystemWeather SystemWeather { get; set; }
}
