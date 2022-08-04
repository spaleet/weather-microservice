using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Infrastructure;
using Service.Models;

namespace Service.Implementations;

public class KafkaService : Intefaces.IKafkaService
{
    private readonly ILogger<KafkaService> _logger;
    private readonly IProducer<string, string> _producer;

    public KafkaService(ILogger<KafkaService> logger, IOptions<KafkaSettings> kafkaSettings)
    {
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync(string city, Weather weather)
    {
        try
        {
            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = string.Format("The temperature in {0}, {1} is currently {2} °C", city, weather.SystemWeather.Country, weather.Main.CelsiusCurrent)
            };
            
            await _producer.ProduceAsync("weather_app", message);
            
            _logger.LogInformation("The temperature in {city}, {country} is currently {temperature} °C", city, weather.SystemWeather.Country, weather.Main.CelsiusCurrent);
        }
        catch (Exception ex)
        {
            _logger.LogError("ERROR : {message}", ex.Message);
        }
    }
}
