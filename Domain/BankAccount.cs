using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain
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
            if (money <= 0) throw new ArgumentOutOfRangeException(nameof(money), "Deposit sum should be more than 0");
            Balance += money;
        }
        public void Withdraw(decimal money) 
        {
            if (money <= 0) throw new ArgumentOutOfRangeException("Sum cannot be less than 1");
            if (!CanWithdraw(money)) throw new InvalidOperationException("Your balance is lower");

            Balance -= money;
        }
        public virtual string PrintInfo()
        {
            return $"ID: {Id}\nNAME: {Name}\nBALANCE: {Balance}\n";
        }
        public virtual bool CanWithdraw(decimal money) 
        {
            return Balance >= money;
        }
    }
}
