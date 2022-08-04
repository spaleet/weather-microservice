namespace Service.Intefaces;

public interface IKafkaService
{
    Task ProduceAsync(string city, Models.Weather weather);
}
