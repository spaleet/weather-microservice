namespace Service.Models;

public class SystemWeather
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }

    [JsonPropertyName("sunriseDateTime")]
    public DateTime SunriseDateTime => DateTimeOffset.FromUnixTimeSeconds(Sunrise).DateTime;

    [JsonPropertyName("sunsetDateTime")]
    public DateTime SunsetDateTime => DateTimeOffset.FromUnixTimeSeconds(Sunset).DateTime;
}
