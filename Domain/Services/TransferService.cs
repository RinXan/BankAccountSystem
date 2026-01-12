using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Domain.Repositories;

namespace BankAccountSystem.Domain.Services
{
    public class TransferService(IAccountRepository bankRepository, ILogger logger)
    {
        private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IAccountRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
        public void Transfer(int fromAccountId, int toAccountId, decimal money)
        {
            if (fromAccountId <= 0) throw new ArgumentOutOfRangeException($"Sender's id: {fromAccountId} is not correct");
            if (toAccountId <= 0) throw new ArgumentOutOfRangeException($"Reciever's id: {toAccountId} is not correct");
            if (toAccountId == fromAccountId) throw new ArgumentException($"Sender: {fromAccountId} and reciever: {toAccountId} cannot have same id");
            if (money <= 0)
            {
                _logger.Log(LogLevel.Warn, "Money amount cannot be lower than 1");
                throw new ArgumentOutOfRangeException("Money for transfer should be more than 0");
            }

            BankAccount from = _bankRepository.GetById(fromAccountId);
            BankAccount to = _bankRepository.GetById(toAccountId);

            if (from == null)
            {
                _logger.Log(LogLevel.Error, "Sender is undefined");
                throw new InvalidOperationException("Sender cannot be null");
            }
            if (to == null)
            {
                _logger.Log(LogLevel.Error, "Reciever is undefined");
                throw new InvalidOperationException("Reciever cannot be null");
            }
            if (!from.CanWithdraw(money))
            {
                _logger.Log(LogLevel.Warn, $"{from.Name} does not have enough money");
                throw new InvalidOperationException($"User {from.Name} does not have enough money");
            }

            from.Withdraw(money);
            to.Deposit(money);
            _logger.Log(LogLevel.Info, $"{from.Name}(ID: {from.Id}) has sent {to.Name}(ID: {to.Id}) - {money}$");
        }
    }
}
