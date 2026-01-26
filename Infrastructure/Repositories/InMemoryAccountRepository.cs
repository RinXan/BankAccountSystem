using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Domain.Exceptions;

namespace BankAccountSystem.Infrastructure.Repositories
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly List<BankAccount> _bankAccounts;

        public InMemoryAccountRepository(List<BankAccount> bankAccounts)
        {
            if (bankAccounts == null) throw new ArgumentNullException(nameof(bankAccounts));
            _bankAccounts = bankAccounts;
        }

        public void Add(BankAccount account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            foreach(BankAccount bankAccount in _bankAccounts)
            {
                if (bankAccount.Id == account.Id) throw new InvalidOperationException($"User with ID: {account.Id} already exists");
            }
            
            _bankAccounts.Add(account);
        }

        public IReadOnlyCollection<BankAccount> GetAll()
        {
            return new List<BankAccount>(_bankAccounts);
        }

        public BankAccount GetById(int id)
        {
            if (id < 1) throw new ArgumentOutOfRangeException(nameof(id));

            foreach (BankAccount account in _bankAccounts)
            {
                if (account.Id == id) return account;
            }

            throw new AccountNotFoundException(id);
        }
        
        public void Update(BankAccount account)
        {
            Console.WriteLine("Not implemented yet. Press any key...");
            Console.ReadKey();
        }
    }
}
