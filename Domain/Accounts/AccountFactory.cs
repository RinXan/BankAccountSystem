using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Exceptions;

namespace BankAccountSystem.Domain.Accounts
{
    public class AccountFactory
    {
        public static BankAccount Create(int id, string name, string type, decimal amount)
        {
            BankAccount account = type switch
            {
                "Savings" => new SavingsAccount(id, name, amount),
                "Credit" => new CreditAccount(id, name, amount),
                _ => throw new UnknownAccontTypeException(type)
            };
            return account;
        }
    }
}
