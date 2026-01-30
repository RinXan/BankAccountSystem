using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Exceptions;

namespace BankAccountSystem.Domain.Accounts
{
    public class CreditAccount : BankAccount
    {
        public decimal CreditLimit { get; private set; }
        public CreditAccount(int id, string name, decimal balance) : base(id, name, balance)
        {
            CreditLimit = 10000;
        }

        protected override void ValidateWithdraw(decimal money)
        {
            decimal remainder = Balance - money;
            if (remainder < -CreditLimit) throw new NotEnoughMoneyException(Id, Balance, money);
        }

        public override string PrintInfo()
        {
            return base.PrintInfo() + $"Credit limit: {CreditLimit}\n";
        }
    }
}
