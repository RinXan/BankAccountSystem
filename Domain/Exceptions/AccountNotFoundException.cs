using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Exceptions
{
    public class AccountNotFoundException : DomainException
    {
        public int AccountId { get; }
        public AccountNotFoundException(int accountId) : base($"Account with ID {accountId} not found")
        {
            AccountId = accountId;
        }
    }
}
