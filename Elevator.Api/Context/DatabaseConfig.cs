using System;
using System.IO;
using System.Text.Json;

namespace Elevator.Api.Context
{
    public class DatabaseConfig
    {
        private readonly string path = "./config/postgresConfig.json";

        public string ReadConfig()
        {
            var postgresConfig = JsonSerializer.Deserialize<PostgresConfig>(File.ReadAllText(path));

            return postgresConfig is null ? throw new Exception($"Can't find postgres config file! Check that {path} contains it.") : postgresConfig.ToString();
        }
    }

    internal class PostgresConfig
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Server={Server};Database={Database};User Id={UserId};Password={Password};Port={Port};";
        }
    }
}
