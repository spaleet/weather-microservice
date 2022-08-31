using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace UnitTests.Helpers;

internal class MockHttpMessageHandler
{
    internal Mock<HttpMessageHandler> SetupResponse<T>(T expectedResponse)
    {
        var mockResponse = new HttpResponseMessage
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)),
            StatusCode = HttpStatusCode.OK
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                 ItExpr.IsAny<HttpRequestMessage>(),
                                                                 ItExpr.IsAny<CancellationToken>())
                                .ReturnsAsync(mockResponse);

        return handlerMock;
    }

    internal Mock<HttpMessageHandler> SetupReturnNotFound()
    {
        var mockResponse = new HttpResponseMessage
        {
            Content = new StringContent(""),
            StatusCode = HttpStatusCode.NotFound
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                 ItExpr.IsAny<HttpRequestMessage>(),
                                                                 ItExpr.IsAny<CancellationToken>())
                                .ReturnsAsync(mockResponse);

        return handlerMock;
    }
}
