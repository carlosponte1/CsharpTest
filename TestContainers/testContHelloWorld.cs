using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using NUnit.Framework;

namespace CsharpPlaywrith.Tests
{
    [TestFixture]
    public class testContHelloWorld
    {
        private IContainer _container;

        [SetUp]
        public async Task SetUp()
        {
            _container = new ContainerBuilder()
                .WithImage("testcontainers/helloworld:1.1.0")
                .WithPortBinding(8080, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
                .Build();

            await _container.StartAsync();
        }

        [Test]
        public async Task Should_Return_Valid_Guid()
        {
            var httpClient = new HttpClient();
            var requestUri = new UriBuilder(Uri.UriSchemeHttp, _container.Hostname, _container.GetMappedPublicPort(8080), "uuid").Uri;
            var guid = await httpClient.GetStringAsync(requestUri).ConfigureAwait(false); ;

            // Valida que la respuesta sea un GUID válido
            guid.Should().NotBeNullOrEmpty();
            //Guid.TryParse(guid, out _).Should().BeTrue();
            Debug.Assert(Guid.TryParse(guid, out _));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _container.StopAsync();
            await _container.DisposeAsync();        
        }
    }
}
