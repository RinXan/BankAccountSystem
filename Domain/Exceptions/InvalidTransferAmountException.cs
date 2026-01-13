using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Exceptions
{
    public class InvalidTransferAmountException : DomainException
    {
        public decimal Amount { get; }
        public InvalidTransferAmountException(decimal amount)
            : base($"Transfer amount must be greater than 0. Current value: {amount}") 
        {
            Amount = amount;
        }
    }
}
