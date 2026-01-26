using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Exceptions;

namespace BankAccountSystem.Domain.Accounts
{
    public abstract class BankAccount
    {
        public int Id { get; }
        public string Name { get; private set; }
        public decimal Balance { get; protected set; }

        public BankAccount(int id, string name) 
        {
            if (id < 1) throw new ArgumentOutOfRangeException(nameof(id), "id is not correct");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name), "name is not correct");
            
            Id = id;
            Name = name;
            Balance = 0;
        }
        
        public void Deposit(decimal money)
        {
            if (money <= 0) throw new InvalidTransferAmountException(money);
            Balance += money;
        }
        
        public void Withdraw(decimal money) 
        {
            if (money <= 0) throw new InvalidTransferAmountException(money);

            ValidateWithdraw(money);

            Balance -= money;
        }
        
        public virtual string PrintInfo()
        {
            return $"ID: {Id}\nNAME: {Name}\nBALANCE: {Balance}\n";
        }
        
        protected abstract void ValidateWithdraw(decimal money);
    }
}
