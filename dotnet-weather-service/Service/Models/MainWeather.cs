namespace Service.Models;

public class MainWeather
{
    [JsonPropertyName("temp")]
    public double Temperature { get; set; }

    [JsonPropertyName("pressure")]
    public double Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public long Humidity { get; set; }

    [JsonPropertyName("temp_min")]
    public double TemperatureMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TemperatureMax { get; set; }
}
