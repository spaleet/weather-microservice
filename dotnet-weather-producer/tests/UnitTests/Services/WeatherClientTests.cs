using Core.Models;
using Core.Settings;
using FluentAssertions;
using Infrastructure.Clients;
using Microsoft.Extensions.Options;

namespace UnitTests.Services;
public class WeatherClientTests
{
    private readonly WeatherClient _sut;
    public WeatherClientTests()
    {
        var config = Options.Create(new WeatherSettings());
        _sut = new WeatherClient(config, new HttpClient());
    }

    [Fact]
    public async Task GetWeatherAsync_WhenCityIsNull_ThrowsArgumentNullException()
    {
        var result = async () => await _sut.GetWeatherAsync(null);

        await result.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetWeatherAsync_WhenCityIsEmpty_ThrowsArgumentNullException()
    {
        var result = async () => await _sut.GetWeatherAsync(string.Empty);

        await result.Should().ThrowAsync<ArgumentNullException>();
    }


}
