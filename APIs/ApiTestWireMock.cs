using System;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Newtonsoft.Json.Linq;
using FluentAssertions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace WireMockExample 
{
    [TestFixture]
    public class ApiTests 
    {
     
        private readonly ApiTestConfig _mockAPI = new ApiTestConfig();
        //private readonly ApiTestConfig _mockAPI = new ApiTestConfig();
        private HttpResponseMessage _response;
        
        public async Task StartServer()
        {
            _mockAPI.StartMockServer();
        }

        public async Task SendAuthRequest()
        {
            var requestData = new StringContent("{\"username\":\"admin\",\"password\":\"password123\"}", System.Text.Encoding.UTF8, "application/json");
            _response = await _mockAPI._client.PostAsync("/auth", requestData);
        }

        public async Task VerifyResponse()
        {
            _response.EnsureSuccessStatusCode();

            var responseBody = await _response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);

            json["token"].Should().NotBeNull();
            System.Console.WriteLine("Mocked Token: " + json["token"]);
        }





        [TearDownAttribute]
        public void TearDown()
        {

            _mockAPI?.StopMockServer();




        }
    }
}
