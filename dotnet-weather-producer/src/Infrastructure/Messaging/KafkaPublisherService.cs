using Confluent.Kafka;
using Core.Interfaces;
using Core.Models;
using Core.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Retry;
using Polly;

namespace Infrastructure.Messaging;

public class KafkaPublisherService : IPublisher
{
    private readonly ILogger<KafkaPublisherService> _logger;
    private readonly IProducer<string, string> _producer;
    private readonly KafkaSettings _kafkaSettings;

    private const int MAX_RETRIES = 3;
    private readonly AsyncRetryPolicy _retry;

    public KafkaPublisherService(ILogger<KafkaPublisherService> logger, IOptions<KafkaSettings> kafkaSettings)
    {
        _logger = logger;
        _kafkaSettings = kafkaSettings.Value;

        var config = new ProducerConfig
        {
            BootstrapServers = _kafkaSettings.BootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();

        _retry = Policy.Handle<Exception>()
                       .WaitAndRetryAsync(
                            retryCount: MAX_RETRIES,
                            sleepDurationProvider: times => TimeSpan.FromSeconds(times),
                            onRetry: (err, sleepDuration, attemptNumber, context) =>
                            {
                                _logger.LogError("ERROR : {message}. Retrying in {sleepDuration}. {attemptNumber} / {max_retries}",
                                                 err.Message,
                                                 sleepDuration,
                                                 attemptNumber,
                                                 MAX_RETRIES);
                            });
    }

    public async Task PublishAsync(string city, Weather weather)
    {
        await _retry.ExecuteAsync(async () =>
        {
            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = string.Format("The temperature in {0}, {1} is currently {2} °C", city, weather.SystemWeather.Country, weather.Main.CelsiusCurrent)
            };

            await _producer.ProduceAsync(_kafkaSettings.Topic, message);

            _logger.LogInformation("The temperature in {city}, {country} is currently {temperature} °C", city, weather.SystemWeather.Country, weather.Main.CelsiusCurrent);
        });
    }
}
