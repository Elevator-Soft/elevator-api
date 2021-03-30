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
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"host={Host};port={Port};database={Database};username={Username};password={Password};";
        }
    }
}
