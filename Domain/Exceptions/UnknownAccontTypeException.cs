using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Exceptions
{
    public class UnknownAccontTypeException : DomainException
    {
        public string AccountType { get; }
        public UnknownAccontTypeException(string type) 
            : base($"Unknown account type: {type}") 
        {
            AccountType = type;
        }
    }
}
