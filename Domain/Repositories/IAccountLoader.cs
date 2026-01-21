using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;

namespace BankAccountSystem.Domain.Repositories
{
    public interface IAccountLoader
    {
        IEnumerable<BankAccount> Load();
    }
}
