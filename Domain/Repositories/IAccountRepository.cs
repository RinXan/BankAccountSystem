using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;

namespace BankAccountSystem.Domain.Repositories
{
    public interface IAccountRepository
    {
        BankAccount GetById(int id);
        IReadOnlyCollection<BankAccount> GetAll();
        void Add(BankAccount account);
    }
}
