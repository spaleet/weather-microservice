namespace Service.Models;

public class BaseWeather
{
    [JsonPropertyName("dt")]
    public long UnixDateTime { get; set; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(UnixDateTime).DateTime;

    [JsonPropertyName("main")]
    public MainWeather Main { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; }

    [JsonPropertyName("weather")]
    public IEnumerable<State> State { get; set; }
}
