using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Dtos;
using BankAccountSystem.Domain.Exceptions;

namespace BankAccountSystem.Domain.Accounts
{
    public class AccountFactory
    {
        public static BankAccount Create(AccountDto accInfo)
        {
            BankAccount account = accInfo.Type switch
            {
                "Savings" => new SavingsAccount(accInfo.Id, accInfo.Name, accInfo.Balance),
                "Credit" => new CreditAccount(accInfo.Id, accInfo.Name, accInfo.Balance),
                _ => throw new UnknownAccontTypeException(accInfo.Type)
            };
            return account;
        }
    }
}
