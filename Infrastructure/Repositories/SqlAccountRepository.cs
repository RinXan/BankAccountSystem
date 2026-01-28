using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Domain.Exceptions;
using Microsoft.Data.Sqlite;

namespace BankAccountSystem.Infrastructure.Repositories
{
    public class SqlAccountRepository : IAccountRepository
    {
        private readonly string DbUrl;
        public SqlAccountRepository(string dbUrl)
        {
            DbUrl = dbUrl;
        }

        public void Add(BankAccount account)
        {
            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO Accounts (Id, Owner, Balance, Type) 
                VALUES ($id, $owner, $balance, $type)
                """;

            command.Parameters.AddWithValue("$id", account.Id);
            command.Parameters.AddWithValue("$owner", account.Name);
            command.Parameters.AddWithValue("$balance", account.Balance);
            command.Parameters.AddWithValue("$type", account.GetType().Name);

            command.ExecuteNonQuery();
        }

        public IReadOnlyCollection<BankAccount> GetAll()
        {
            List<BankAccount> accounts = new List<BankAccount>();

            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Owner, Balance, Type FROM Accounts";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                accounts.Add(CreateAccount(reader));
            }

            return accounts;
        }

        public BankAccount GetById(int id)
        {
            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = """
                SELECT Id, Owner, Balance, Type
                FROM Accounts 
                WHERE Id = $id
                """;
            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read()) throw new AccountNotFoundException(id);

            return CreateAccount(reader);
        }

        public void Update(BankAccount account)
        {
            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = """
                UPDATE Accounts
                SET Balance = $balance
                WHERE Id = $id
                """;

            command.Parameters.AddWithValue("$balance", account.Balance);
            command.Parameters.AddWithValue("$id", account.Id);

            command.ExecuteNonQuery();
        }

        private static BankAccount CreateAccount(SqliteDataReader reader) 
        {
            int id = reader.GetInt32(0);
            string owner = reader.GetString(1);
            decimal balance = reader.GetDecimal(2);
            string type = reader.GetString(3);

            return type switch
            {
                "Savings" => new SavingsAccount(id, owner, balance),
                "Credit" => new CreditAccount(id, owner, balance),
                _ => throw new UnknownAccontTypeException(type)
            };
        }

        public void Transfer(int fromId, int toId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
