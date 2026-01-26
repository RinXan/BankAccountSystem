using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;

namespace BankAccountSystem.Infrastructure.Repositories
{
    public class SqlAccountRepository : IAccountRepository
    {
        public void Add(BankAccount account)
        {
            Console.WriteLine("Not implemented yet. Press any key...");
            Console.ReadKey();
        }

        public IReadOnlyCollection<BankAccount> GetAll()
        {
            throw new NotImplementedException();
        }

        public BankAccount GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(BankAccount account)
        {
            Console.WriteLine("Not implemented yet. Press any key...");
            Console.ReadKey();
        }
    }
}
