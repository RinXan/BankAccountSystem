using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Exceptions
{
    public class NotEnoughMoneyException : DomainException
    {
        public int AccountId { get; }
        public decimal Balance { get; }
        public decimal RequestedMoney { get; }
        public NotEnoughMoneyException(int fromAccountId, decimal balance, decimal requestedMoney)
            : base($"User [{fromAccountId}]: cannot withdraw {requestedMoney}." +
                  $"Current balance: {balance}.") 
        {
            AccountId = fromAccountId;
            Balance = balance;
            RequestedMoney = requestedMoney;
        }
    }
}
