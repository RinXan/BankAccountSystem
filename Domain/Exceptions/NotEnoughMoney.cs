using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Exceptions
{
    public class NotEnoughMoney(string message) : DomainException(message) { }
}
