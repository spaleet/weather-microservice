namespace Core.Interfaces;

public interface IPublisher
{
    Task PublishAsync(string city, Weather weather);
}