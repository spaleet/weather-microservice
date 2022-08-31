using Core.Models;
using Core.Settings;
using FluentAssertions;
using Infrastructure.Clients;
using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using UnitTests.Helpers;

namespace UnitTests.Services;
public class WeatherClientTests
{
    private readonly IOptions<WeatherSettings> _config;
    private readonly MockHttpMessageHandler _handlerMock;
    public WeatherClientTests()
    {
        _config = Options.Create(new WeatherSettings());
        _handlerMock = new MockHttpMessageHandler();
    }

    [Fact]
    public async Task GetWeatherAsync_WhenCityIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var handlerMock = _handlerMock.SetupResponse(new Weather());
        var client = new HttpClient(handlerMock.Object);

        var sut = new WeatherClient(_config, client);

        // Act
        var result = async () => await sut.GetWeatherAsync(null);

        // Assert
        await result.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetWeatherAsync_WhenCityIsEmpty_ThrowsArgumentNullException()
    {
        // Arrange
        var handlerMock = _handlerMock.SetupResponse(new Weather());
        var client = new HttpClient(handlerMock.Object);

        var sut = new WeatherClient(_config, client);

        // Act
        var result = async () => await sut.GetWeatherAsync(string.Empty);

        // Assert
        await result.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetWeatherAsync_InvalidCity_ReturnsNotFound()
    {
        // Arrange
        var handlerMock = _handlerMock.SetupReturnNotFound();
        var client = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://example.com")
        };

        var sut = new WeatherClient(_config, client);

        // Act
        var result = async () => await sut.GetWeatherAsync("Invalid City");

        // Assert
        await result.Should().ThrowAsync<HttpRequestException>();
        handlerMock.Protected()
                    .Verify("SendAsync",
                            Times.Exactly(1),
                            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetWeatherAsync_InvalidCity_ReturnsResponse()
    {
        // Arrange
        var handlerMock = _handlerMock.SetupResponse(new Weather());
        var client = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://example.com")
        };

        var sut = new WeatherClient(_config, client);

        // Act
        var result = await sut.GetWeatherAsync("chicago");

        // Assert
        handlerMock.Protected()
                    .Verify("SendAsync",
                            Times.Exactly(1),
                            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                            ItExpr.IsAny<CancellationToken>());
        result.Should().BeOfType<Weather>();
    }


}
