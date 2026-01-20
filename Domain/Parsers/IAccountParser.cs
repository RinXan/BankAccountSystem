using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Dtos;

namespace BankAccountSystem.Domain.Parsers
{
    public interface IAccountParser
    {
        AccountDto Parse(string line);
    }
}
