namespace Core.Models.Settings;

public record KafkaSettings
{
    public string BootstrapServers { get; init; }
}
