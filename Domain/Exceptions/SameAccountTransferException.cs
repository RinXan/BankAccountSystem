using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Exceptions
{
    public class SameAccountTransferException : DomainException
    {
        public int AccountId { get; }
        public SameAccountTransferException(int accountId) : base($"Transfer to the same account is not allowed. Account ID {accountId}")
        {
            AccountId = accountId;
        }
    }
}
