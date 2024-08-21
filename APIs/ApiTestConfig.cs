using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;



    public class ApiTestConfig
    {
    public WireMockServer _mockServer;
    public HttpClient _client { get; private set; }

    public void StartMockServer()
        {
        _mockServer = WireMockServer.Start();

        if (_mockServer == null || _mockServer.Urls == null || _mockServer.Urls.Length == 0)
        {
            throw new InvalidOperationException("WireMock server failed to start or no URLs are available.");
        }

        _client = new HttpClient
        {
            BaseAddress = new Uri(_mockServer.Urls[0])
        };




        _mockServer
                .Given(
                    Request.Create().WithPath("/auth").UsingPost())
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("{ \"token\": 8888 }"));
        _mockServer
                .Given(
                    Request.Create().WithPath("/booking").UsingGet())
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("{ \"token\": 8888 }"));


    }

    public void StopMockServer()
    {
        if (_mockServer != null)
        {
            _mockServer.Stop();
            _mockServer.Dispose();

        }

       // _client?.Dispose();
    }
}

