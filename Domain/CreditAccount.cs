using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain
{
    public class CreditAccount : BankAccount
    {
        public decimal CreditLimit { get; private set; }
        public CreditAccount(int id, string name, decimal creditLimit) : base(id, name)
        {
            if (creditLimit < 0) throw new ArgumentException("Credit limit is less than 0");
            CreditLimit = creditLimit;
        }

        public override bool CanWithdraw(decimal money)
        {
            decimal remainder = Balance - money;
            return remainder >= -CreditLimit;
        }

        public override string PrintInfo()
        {
            return base.PrintInfo() + $"Credit limit: {CreditLimit}\n";
        }
    }
}
