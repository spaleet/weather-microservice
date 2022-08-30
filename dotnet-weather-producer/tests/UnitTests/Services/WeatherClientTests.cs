using Core.Settings;
using Microsoft.Extensions.Options;

namespace UnitTests.Services;
public class WeatherClientTests
{
    private readonly IOptions<WeatherSettings> _config;
    public WeatherClientTests()
    {
        _config = Options.Create(new WeatherSettings());
    }


}
