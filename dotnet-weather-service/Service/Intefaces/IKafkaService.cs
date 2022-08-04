namespace Service.Intefaces;

public interface IKafkaService
{
    Task ProduceAsync(Models.Weather weather);
}
