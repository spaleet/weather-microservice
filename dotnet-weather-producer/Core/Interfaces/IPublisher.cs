namespace Core.Interfaces;

public interface IPublisher
{
    Task ProduceAsync(string city, Weather weather);
}