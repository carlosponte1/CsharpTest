using System;
using System.Data;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotPulsar.Internal;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using Testcontainers.MySql;

namespace CsharpPlaywrith.TestContainers
{

    public class ContConfig
    {
        private IContainer _mysqlContainer, _pulsarContainer, _rabbitMqContainer;
        public string _constring;
        public string ServiceUrl;
        public string RabbitMqConnectionString;

        public async Task ContainerSetup()
        {
            _mysqlContainer = new ContainerBuilder()
                    .WithImage("mysql:latest")
                    .WithPortBinding(3306, true)
                .WithEnvironment("MYSQL_ROOT_PASSWORD", "root")
                .WithEnvironment("MYSQL_DATABASE", "testdb")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
                .Build();

            await _mysqlContainer.StartAsync();
            var host = _mysqlContainer.Hostname;
            var port = _mysqlContainer.GetMappedPublicPort(3306);
            _constring = $"Server={host};Port={port};Database=testdb;User=root;Password=root;";


        }

        public async Task PulsarContSetup()
        {
            _pulsarContainer = new ContainerBuilder()
        .WithImage("apachepulsar/pulsar:latest")
        .WithPortBinding(6650, 6650)  //  puerto 6650 container and host mapping true select random port 
        .WithWaitStrategy(Wait.ForUnixContainer()
        .UntilPortIsAvailable(6650))// Espera a que el puerto esté disponible
        //.UntilHttpRequestIsSucceeded(r => r.ForPort(6650)))
        //.UntilContainerIsHealthy())  
        .WithCommand("/bin/bash", "-c", "bin/pulsar standalone")
       // .WithCommand("/bin/bash", "-c", "bin/pulsar-admin namespaces create public/default")
        .Build();


            
            await _pulsarContainer.StartAsync();
            await _pulsarContainer.ExecAsync(new[] { "/bin/bash", "-c", "bin/pulsar-admin namespaces create public/default" });
            var host = _pulsarContainer.Hostname;
            var port = _pulsarContainer.GetMappedPublicPort(6650);
            //ServiceUrl = $"pulsar://{host}:{port}";
            
            ServiceUrl = $"pulsar://localhost:6650";
            // await Task.Delay(3000);
            //await ExecuteCommandInContainer("bin/pulsar-admin namespaces create public/default");
            //await _pulsarContainer.ExecAsync(new[] {"bin/pulsar-admin namespaces create public/default"});
            //await _pulsarContainer.ExecAsync(new[] { "/bin/bash", "-c", "bin/pulsar standalone" });
            // await _pulsarContainer.ExecAsync(new[] { "/bin/bash", "-c", "bin/pulsar-admin namespaces create public/default" });


        }

        /* private async Task ExecuteCommandInContainer(string command)
         {
             try
             {
                 var result = await _pulsarContainer.ExecAsync(new[] { "/bin/bash", "-c", command });
                 Console.WriteLine($"Comando ejecutado: {command}\nResultado: {result.ExitCode}");
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error al ejecutar comando en el contenedor: {ex.Message}");
             }
         }
        */

        public async Task SetupRabbitMqContainer()
        {
            _rabbitMqContainer = new ContainerBuilder()
                .WithImage("rabbitmq:management")  // Usamos la imagen oficial de RabbitMQ
                .WithPortBinding(5672, 5672)        // Puerto para enviar/recibir mensajes
                .WithPortBinding(15672, 15672)       // Puerto para acceder al panel de administración (opcional)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))  // Esperamos hasta que el puerto esté disponible
                .Build();

            await _rabbitMqContainer.StartAsync();  // Iniciar el contenedor
            var host = _rabbitMqContainer.Hostname;
            var port = _rabbitMqContainer.GetMappedPublicPort(5672);
            RabbitMqConnectionString = $"amqp://guest:guest@{host}:{port}/";  // Guardamos la cadena de conexión
        }








        public async Task TearDown()
        {
            await _mysqlContainer.StopAsync();
            await _mysqlContainer.DisposeAsync(); 
            await _pulsarContainer.StopAsync();
            await _pulsarContainer.DisposeAsync();


        }

    }
}
