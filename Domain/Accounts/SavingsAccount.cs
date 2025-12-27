using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Accounts
{
    public class SavingsAccount : BankAccount
    {
        public decimal MinimumBalance { get; private set; }
        public SavingsAccount(int id, string name, decimal minimumBalance) : base(id, name)
        {
            if (minimumBalance < 0) throw new ArgumentException("Minimal saving cannot be less than 0", nameof(minimumBalance));
            MinimumBalance = minimumBalance;
        }

        public override string PrintInfo()
        {
            return base.PrintInfo() + $"Minimal savings: {MinimumBalance}\n";
        }
        public override bool CanWithdraw(decimal money)
        {
            decimal reminder = Balance - money;
            return reminder >= MinimumBalance;
        }
    }
}
