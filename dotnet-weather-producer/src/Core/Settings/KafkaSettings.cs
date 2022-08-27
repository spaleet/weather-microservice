namespace Core.Settings;

public record KafkaSettings
{
    public string BootstrapServers { get; init; }
}
