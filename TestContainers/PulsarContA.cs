using System;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using DotPulsar.Schemas;
using DotNet.Testcontainers.Builders;
using System.Buffers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CsharpPlaywrith.TestContainers
{
    [TestFixture]
    public class PulsarContA
    {
        private ContConfig _contConfig = new ContConfig();
        private IPulsarClient _pulsarClient;
        private IProducer<ReadOnlySequence<byte>> _producer;
        private IConsumer<ReadOnlySequence<byte>> _consumer;
        private readonly string _topic = $"persistent://public/default/mytopic";




        public async Task StartPulsarCon()
        {
            await _contConfig.PulsarContSetup();
        }

        public async Task CreateProdCons()
        {

            Console.WriteLine(_contConfig.ServiceUrl);
            _pulsarClient = PulsarClient.Builder()
                    .ServiceUrl(new Uri(_contConfig.ServiceUrl)) 
                    .Build();

                // Crear productor y consumidor 
                _producer = _pulsarClient.NewProducer()
                                         .Topic(_topic)
                                         .Create();

                _consumer = _pulsarClient.NewConsumer()
                                         .Topic(_topic)                     // El tema del cual consumir mensajes
                                         .SubscriptionName("my-subscription")   // La suscripción a usar
                                         .SubscriptionType(SubscriptionType.Exclusive) // El tipo de suscripción (en este caso, exclusiva)
                                         .Create();

            await Task.Delay(5000);


        }
        
        public async Task SendMessage()
        {
            
                var message = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("{\"type\": \"simple\", \"content\": \"Hello, Pulsar World\"}"));

                await _producer.Send(message);
                Console.WriteLine("Mensaje enviado!"+ message);
        }
        
        public async Task ReceiveMessage()
        {

            /*await foreach (var message in _consumer.Messages())
            {
                var rmessage = Encoding.UTF8.GetString(message.Data.ToArray());
                Console.WriteLine($"Mensaje recibido: {Encoding.UTF8.GetString(message.Data.ToArray())}");
                await _consumer.Acknowledge(message);
                //rmessage.Should().NotBeSameAs.
                break; // Romper el bucle después de recibir el primer mensaje
            }*/
            // Recibe un único mensaje
            var message = await _consumer.Receive();

            // Procesa el mensaje recibido
            var rmessage = Encoding.UTF8.GetString(message.Data.ToArray());
            Console.WriteLine($"Mensaje recibido: {rmessage}");

            // Reconoce el mensaje
            await _consumer.Acknowledge(message);
            Console.WriteLine("Mensaje reconocido.");


        }


        [Test]
        public async Task PulsaExecuteA()
        {
            await StartPulsarCon();
            await CreateProdCons();
            await SendMessage();
          await ReceiveMessage();  // Aseguramos recibir el mensaje después de enviarlo
          await ClosePulsarTest();
        }

        [TearDown]
        
        public async Task ClosePulsarTest()
        {
            if (_producer != null)
                await _producer.DisposeAsync();
            if (_consumer != null)
                await _consumer.DisposeAsync();
            if (_pulsarClient != null)
                await _pulsarClient.DisposeAsync();

           
        }

    }
}
