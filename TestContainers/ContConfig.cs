using System;
using System.Data;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using Testcontainers.MySql;

namespace CsharpPlaywrith.TestContainers
{
    
    public class ContConfig
    {
        private IContainer _mysqlContainer;
        public string _constring;

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


        public async Task TearDown()
        {
            await _mysqlContainer.StopAsync();
            await _mysqlContainer.DisposeAsync();
        }

    }
}
