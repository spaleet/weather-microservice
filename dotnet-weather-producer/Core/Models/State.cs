namespace Core.Models;

public class State
{
    [JsonPropertyName("main")]
    public string MainWeather { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    private string _icon;
    [JsonPropertyName("icon")]
    public string Icon
    {
        get => $"https://openweathermap.org/img/w/{_icon}.png";
        set => _icon = value;
    }
}
