using Service.Models;

namespace Service.Implementations;

public class KafkaService : Intefaces.IKafkaService
{
    public KafkaService()
    {

    }

    public Task ProduceAsync(Weather weather)
    {
        throw new NotImplementedException();
    }
}
