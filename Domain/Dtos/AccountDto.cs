using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Dtos
{
    public class AccountDto(int id, string name, string type, decimal balance)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public string Type { get; } = type;
        public decimal Balance { get; } = balance;
    }
}
