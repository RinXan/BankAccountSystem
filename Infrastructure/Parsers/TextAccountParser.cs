using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Parsers;
using BankAccountSystem.Domain.Dtos;

namespace BankAccountSystem.Infrastructure.Parsers
{
    public class TextAccountParser : IAccountParser
    {
        public AccountDto Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) throw new ArgumentNullException(nameof(line));

            string[] parts = line.Split('_');

            if (parts.Length != 4) throw new InvalidOperationException($"Expected 4 parts but got {parts.Length}");

            string type = parts[0];
            int id = int.Parse(parts[1]);
            string name = parts[2];
            decimal balance = decimal.Parse(parts[3]);

            return new AccountDto(id, name, type, balance);
        }
    }
}
