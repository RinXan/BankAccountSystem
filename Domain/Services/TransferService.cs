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
        public void Transfer(BankAccount from, BankAccount to, decimal money)
        {
            if (from == null) throw new ArgumentNullException(nameof(from), "Transfer objects cannot be null"); 
            if (to == null) throw new ArgumentNullException(nameof(to), "Transfer objects cannot be null"); 
            if (from.Id == to.Id) throw new InvalidOperationException("Cannot transfer money from one account to itself");
            if (money <= 0) throw new ArgumentOutOfRangeException("Money for transfer should be more than 0");
            if (!from.CanWithdraw(money)) throw new InvalidOperationException($"User {from.Name} does not have enough money");

            from.Withdraw(money);
            to.Deposit(money);
        }
    }
}
