using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Dtos;
using BankAccountSystem.Domain.Parsers;
using BankAccountSystem.Domain.Repositories;

namespace BankAccountSystem.Infrastructure.Repositories
{
    public class FileAccountLoader : IAccountLoader
    {
        private readonly string _filePath;
        private readonly IAccountParser _parser;
        public FileAccountLoader(string filePath, IAccountParser parser) 
        {
            _filePath = filePath ?? throw new InvalidOperationException($"FIle path is not correct: {filePath}");
            _parser = parser ?? throw new InvalidOperationException($"Parser is not provided: {filePath}");
        }

        public IEnumerable<BankAccount> Load()
        {
            List<BankAccount> accounts = new List<BankAccount>();

            foreach (string line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                AccountDto accDto = _parser.Parse(line);
                BankAccount account = AccountFactory.Create(accDto);

                accounts.Add(account);
            }

            return accounts;
        }
    }
}
