namespace Service.Interfaces;

public interface IKafkaService
{
    Task ProduceAsync(string city, Models.Weather weather);
}
