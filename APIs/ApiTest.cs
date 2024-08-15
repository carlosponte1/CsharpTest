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
            // Configuración del endpoint
            var endpoint = "/booking";

            // Realizando la solicitud GET al endpoint
            var response = await _client.GetAsync(endpoint);

            // Afirmación: Verifica que el código de estado de la respuesta sea exitoso (2xx)
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK,
                "porque esperamos una respuesta exitosa del servidor");

            // Afirmación: Verifica que el tipo de contenido sea JSON
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json",
                "porque la respuesta debería estar en formato JSON");

            // Leyendo el cuerpo de la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();

            // Afirmación: Verifica que el cuerpo de la respuesta no sea nulo ni vacío
            responseBody.Should().NotBeNullOrEmpty("porque la respuesta debería contener datos");

            // Parseando el JSON de la respuesta
            var jsonArray = JArray.Parse(responseBody);

            // Afirmación: Verifica que el JSON no esté vacío
            jsonArray.Should().NotBeEmpty("porque debería haber al menos una reserva en el sistema");

            var firstBooking = jsonArray.First;
            firstBooking.Should().NotBeNull("porque debería haber al menos una reserva");
            firstBooking["bookingid"].Should().NotBeNull("porque cada reserva debería tener un ID de reserva");

            
        }



        [TearDown]
        public void Teardown()
        {
            _client.Dispose();
        }
    }
}
