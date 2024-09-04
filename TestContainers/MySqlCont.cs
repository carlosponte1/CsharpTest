using System.Data;
using FluentAssertions;
using MySql.Data.MySqlClient;


namespace CsharpPlaywrith.TestContainers
{
    [TestFixture]
    public class MySqlCont

    {
        private ContConfig _contConfig = new ContConfig();
        private MySqlConnection connection;
        private MySqlCommand command;
       

        public async Task SqlContStart()

        {
            try
            {
                await _contConfig.ContainerSetup();
                connection = new MySqlConnection(_contConfig._constring);
                connection.Open();
                Console.WriteLine("Conexión exitosa");
                connection.State.Should().Be(ConnectionState.Open, "la conexión a la base de datos esta activa.");


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");

            }

        }

        public async Task SqlOpen()
        {
            string createTableQuery = @"
                                CREATE TABLE IF NOT EXISTS users 
            (
                                id INT PRIMARY KEY AUTO_INCREMENT,
                                user VARCHAR(50) NOT NULL,
                                password VARCHAR(255) NOT NULL,
                                description TEXT
            );";
            string insertUsersQuery = @"
    INSERT INTO users (user, password, description) VALUES
    ('standard_user', 'secret_sauce', NULL),
    ('locked_out_user', 'secret_sauce', NULL),
    ('problem_user', 'secret_sauce', NULL),
    ('performance_glitch_user', 'secret_sauce', NULL),
    ('error_user', 'secret_sauce', NULL),
    ('visual_user', 'secret_sauce', NULL);";
            command = new MySqlCommand(createTableQuery, connection);
             await command.ExecuteNonQueryAsync();
            command = new MySqlCommand(insertUsersQuery, connection);
            await command.ExecuteNonQueryAsync();
          /*  using (var command = new MySqlCommand(createTableQuery, connection))
            {
                await command.ExecuteNonQueryAsync();
            }

            using (var command = new MySqlCommand(insertUsersQuery, connection))
            {
                await command.ExecuteNonQueryAsync();
            }*/



        }
        
        public async Task ValidateCon()
        {
            string[] usernames = {
    "standard_user",
    "locked_out_user",
    "problem_user",
    "performance_glitch_user",
    "error_user",
    "visual_user"
};
            foreach (var username in usernames)
            {
                string query = $"SELECT user FROM users WHERE user = '{username}'";

                command = new MySqlCommand(query, connection);
                {
                    var result = await command.ExecuteScalarAsync();
                    result.Should().NotBeNull($"El usuario '{username}' no existe o es Null  en la base de datos.");
                }
            }


        }
        [TearDownAttribute]
        public async Task closeCon()
        {
            connection.CloseAsync();
            command.Dispose();
        }
        [Test]
        public async Task ExecuteTestContainerTest()
        {
            await SqlContStart();
            await SqlOpen();
            await ValidateCon();
            await closeCon();
            
           

        }
        
        

    }

    

    }












