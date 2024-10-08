﻿using ProtoBuf.Serializers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CsharpPlaywrith.TestContainers
{
    public class RabbitmqCont
    {
        private ContConfig _contConfig = new ContConfig();
        //private readonly _factory;
        private ConnectionFactory _factory; 
        private  IConnection _connection;     
        private  IModel _channel;


       
        /*public RabbitmqCont()
        {
            
            
            // Inicializamos 'factory' una vez con la información de conexión a RabbitMQ
       
        }*/
        public async Task starRabbitmqCont()
        {
            await _contConfig.SetupRabbitMqContainer();
            //string connectionString = _contConfig.RabbitMqConnectionString;
            _factory = new ConnectionFactory { Uri = new Uri(_contConfig.RabbitMqConnectionString) };

            // se crea la  conexión y el canal para usar en todas las funciones
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        // Función para enviar un mensaje
        public void SendMessage(string message)
        {
            // Declaramos la cola 'testQueue' 
            _channel.QueueDeclare("testQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Convertimos el mensaje a bytes
            var body = Encoding.UTF8.GetBytes(message);

            // Publicamos el mensaje en la cola 'testQueue'
            _channel.BasicPublish(exchange: "", routingKey: "testQueue", basicProperties: null, body: body);

            
            Console.WriteLine($"[x] Sent {message}");
        }

        // Función para recibir un mensaje
        public void ReceiveMessage()
        {
            // Declaramos la cola 'testQueue' (si no existe, la creamos)
            _channel.QueueDeclare("testQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Preparamos el consumido,
            var consumer = new EventingBasicConsumer(_channel);

            // Aquí le decimos qué hacer cuando llega un mensaje
            consumer.Received += (model, ea) =>
            {
                // Obtenemos el cuerpo del mensaje (en bytes) y lo convertimos en texto
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Mostramos en la consola el mensaje recibido
                message.Should().Be("Hola conejito");
                Console.WriteLine($"[x] Received {message}");
            };

            // Empezamos a "escuchar" la cola y recibir mensajes
           
            _channel.BasicConsume(queue: "testQueue", autoAck: true, consumer: consumer);
        }
       


        [Test]
        public async Task RabbitExecute()
        {
        await starRabbitmqCont();
              
              SendMessage("Hola conejito");
              ReceiveMessage();
              Close();
        }
        [TearDownAttribute]
        public void Close()
        {
            _channel.Dispose();
            _connection.Dispose();

        }



    }
}
