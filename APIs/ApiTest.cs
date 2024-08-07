using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.TestPlatform.Common;
using System.Net;
using Newtonsoft.Json;

namespace CsharpPlaywrith.APIs
{
    [TestFixture]
    internal class ApiTest
    {
        private HttpClient _client;
        private string _token;
        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("https://restful-booker.herokuapp.com");
        }

        [Test]
        public async Task SendAndGetRequestToken()
        {
            var endpoint = "/auth";
            var requestData = new StringContent("{\"username\":\"admin\",\"password\":\"password123\"}", System.Text.Encoding.UTF8, "application/json");

             var response = await _client.PostAsync(endpoint, requestData);
            // Assert
            response.EnsureSuccessStatusCode();
            //response.StatusCode.Should().Be(200,because:"succes");
            
            var responseBody = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseBody);
            json["token"].Should().NotBeNull();

            
            _token = json["token"].ToString();
            System.Console.WriteLine("Token: " + _token+" "+response.StatusCode);


        }

        [Test]
        public async Task GetBookingID()
        {
            var endpoint = "/booking";
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            //var json = JArray.Parse(responseBody);
            

            using StreamReader reader = new(responseBody);
            string jsonString = reader.ReadToEnd();
            var json = JsonConvert.DeserializeObject<Object>(jsonString);
            json = JObject.Parse(responseBody);


        }


        [TearDown]
        public void Teardown()
        {
            _client.Dispose();
        }
    }
}
