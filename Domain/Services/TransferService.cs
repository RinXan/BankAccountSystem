using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;

namespace BankAccountSystem.Domain.Services
{
    public class TransferService
    {
        public static void Transfer(BankAccount from, BankAccount to, decimal money)
        {
            if (from.Id == to.Id) throw new ArgumentException("Cannot transfer money from one account to itself");
            if (money <= 0) throw new ArgumentException("Money for transfer should be more than 0");
            if (!from.CanWithdraw(money)) throw new ArgumentException($"User {from.Name} does not have enough money");

            from.Withdraw(money);
            to.Deposit(money);
        }
    }
}
