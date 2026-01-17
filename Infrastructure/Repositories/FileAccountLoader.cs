using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;

namespace BankAccountSystem.Infrastructure.Repositories
{
    public class FileAccountLoader : IAccountLoader
    {
        private readonly string _filePath;
        public FileAccountLoader(string filePath) 
        {
            _filePath = filePath ?? throw new InvalidOperationException();
        }

        public IEnumerable<BankAccount> Load()
        {
            List<BankAccount> accounts = new List<BankAccount>();

            foreach (string line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split('_');

                string type = parts[0];
                int id = Convert.ToInt32(parts[1]);
                string name = parts[2];
                decimal amount = Convert.ToDecimal(parts[3]);

                BankAccount account = AccountFactory.Create(id, name, type, amount);

                accounts.Add(account);
            }

            return accounts;
        }
    }
}
