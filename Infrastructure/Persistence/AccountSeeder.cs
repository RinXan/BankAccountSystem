using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;

namespace BankAccountSystem.Infrastructure.Persistence
{
    public class AccountSeeder
    {
        public static void Seed(IAccountRepository repository)
        {
            if (repository.GetAll().Count > 0) return;

            repository.Add(new CreditAccount(1, "Olesya", 1000));
            repository.Add(new SavingsAccount(2, "Anna", 200));
            repository.Add(new CreditAccount(3, "Julia", 30000));
        }
    }
}
