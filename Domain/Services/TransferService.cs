using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Exceptions;
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
            if (fromAccountId <= 0) throw new ArgumentOutOfRangeException(nameof(fromAccountId));
            if (toAccountId <= 0) throw new ArgumentOutOfRangeException(nameof(toAccountId));
            if (toAccountId == fromAccountId) throw new SameAccountTransferException(toAccountId);
            if (money <= 0) throw new InvalidTransferAmountException(money);

            BankAccount from = _bankRepository.GetById(fromAccountId);
            BankAccount to = _bankRepository.GetById(toAccountId);

            if (from == null) throw new AccountNotFoundException(fromAccountId);
            if (to == null) throw new AccountNotFoundException(toAccountId);
            if (!from.CanWithdraw(money)) throw new NotEnoughMoneyException(from.Id, from.Balance, money);

            from.Withdraw(money);
            to.Deposit(money);
            _logger.Log(LogLevel.Info, $"{from.Name}(ID: {from.Id}) has sent {to.Name}(ID: {to.Id}) - {money}$");
        }
    }
}
