using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Logger;

namespace BankAccountSystem.Domain.Services
{
    public class TransferService(ILogger logger)
    {
        private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public void Transfer(BankAccount from, BankAccount to, decimal money)
        {
            if (from == null)
            {
                _logger.Log(LogLevel.Error, "Sender is undefined");
                throw new ArgumentNullException(nameof(from), "Sender cannot be null");
            }
            if (to == null)
            {
                _logger.Log(LogLevel.Error, "Reciever is undefined");
                throw new ArgumentNullException(nameof(to), "Reciever cannot be null");
            }
            if (from.Id == to.Id)
            {
                _logger.Log(LogLevel.Warn, $"User: {from.Id} cannot send money to user: {to.Id}");
                throw new InvalidOperationException("Cannot transfer money from one account to itself");
            }
            if (money <= 0)
            {
                _logger.Log(LogLevel.Warn, "Money amount cannot be lower than 1");
                throw new ArgumentOutOfRangeException("Money for transfer should be more than 0");
            }
            if (!from.CanWithdraw(money))
            {
                _logger.Log(LogLevel.Warn, $"{from.Name} does not have enough money");
                throw new InvalidOperationException($"User {from.Name} does not have enough money");
            }

            _logger.Log(LogLevel.Info, "Sending money...");
            from.Withdraw(money);
            to.Deposit(money);
            _logger.Log(LogLevel.Info, $"{from.Name} has sent {to.Name} - {money}$");
        }
    }
}
