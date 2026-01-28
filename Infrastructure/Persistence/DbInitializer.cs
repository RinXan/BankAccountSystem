using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace BankAccountSystem.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(string url)
        {
            using var connection = new SqliteConnection(url);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
                CREATE TABLE IF NOT EXISTS Accounts (
                    Id INTEGER PRIMARY KEY,
                    Owner TEXT NOT NULL,
                    Balance REAL NOT NULL,
                    Type TEXT NOT NULL
                );
                """;

            command.ExecuteNonQuery();
        }
    }
}
