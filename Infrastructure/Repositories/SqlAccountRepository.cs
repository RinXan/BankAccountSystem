using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Dtos;
using BankAccountSystem.Domain.Exceptions;
using BankAccountSystem.Domain.Repositories;
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
                INSERT INTO Accounts (Id, Name, Balance, Type) 
                VALUES ($id, $name, $balance, $type)
                """;

            command.Parameters.AddWithValue("$id", account.Id);
            command.Parameters.AddWithValue("$name", account.Name);
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
            command.CommandText = "SELECT Id, Name, Balance, Type FROM Accounts";

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
                SELECT Id, Name, Balance, Type
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

        public void Transfer(int fromId, int toId, decimal amount)
        {
            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var from = LoadAccount(connection, transaction, fromId);
                var to = LoadAccount(connection, transaction, toId);

                from.Withdraw(amount);
                to.Deposit(amount);

                UpdateAccount(connection, transaction, from);
                UpdateAccount(connection, transaction, to);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback(); ;
                throw;
            }
        }

        public void Deposit(int accountId, decimal amount)
        {
            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var account = LoadAccount(connection, transaction, accountId);

                account.Deposit(amount);

                UpdateAccount(connection, transaction, account);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Withdraw(int accountId, decimal amount)
        {
            using var connection = new SqliteConnection(DbUrl);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var account = LoadAccount(connection, transaction, accountId);

                account.Withdraw(amount);

                UpdateAccount(connection, transaction, account);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        
        // Helpers *********************
        private static BankAccount CreateAccount(SqliteDataReader reader) 
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            decimal balance = reader.GetDecimal(2);
            string type = reader.GetString(3);

            return type switch
            {
                "SavingsAccount" => new SavingsAccount(id, name, balance),
                "CreditAccount" => new CreditAccount(id, name, balance),
                _ => throw new UnknownAccontTypeException(type)
            };
        }
        
        private BankAccount LoadAccount(SqliteConnection connection, SqliteTransaction transaction, int id)
        {
            using var cmd = connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = @"SELECT Id, Name, Type, Balance
                                FROM Accounts
                                WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read()) throw new AccountNotFoundException(id);

            return AccountFactory.Create(new AccountDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDecimal(3)));

        }
    
        private void UpdateAccount(SqliteConnection connection, SqliteTransaction transaction, BankAccount account)
        {
            using var cmd = connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = 
                @"UPDATE Accounts
                  SET Balance = @balance
                  WHERE Id = @id";

            cmd.Parameters.AddWithValue("@balance", account.Balance);
            cmd.Parameters.AddWithValue("@id", account.Id);

            cmd.ExecuteNonQuery();
        }

    }
}
