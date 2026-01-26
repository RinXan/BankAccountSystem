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
    public class AccountService(IAccountRepository bankRepository, ILogger logger)
    {
        private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IAccountRepository _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
        public void Transfer(int fromAccountId, int toAccountId, decimal money)
        {
            if (fromAccountId <= 0) throw new ArgumentOutOfRangeException(nameof(fromAccountId));
            if (toAccountId <= 0) throw new ArgumentOutOfRangeException(nameof(toAccountId));
            if (toAccountId == fromAccountId) throw new SameAccountTransferException(toAccountId);

            BankAccount from = _bankRepository.GetById(fromAccountId);
            BankAccount to = _bankRepository.GetById(toAccountId);

            if (from == null) throw new AccountNotFoundException(fromAccountId);
            if (to == null) throw new AccountNotFoundException(toAccountId);

            from.Withdraw(money);
            to.Deposit(money);
            _logger.Log(LogLevel.Info, $"{from.Name}(ID: {from.Id}) has sent {to.Name}(ID: {to.Id}) - {money}$");
        }
    
        public BankAccount Deposit(int accountId, decimal amount)
        {
            if (amount <= 0) throw new InvalidTransferAmountException(amount);

            BankAccount account = _bankRepository.GetById(accountId);

            account.Deposit(amount);

            return account;
        }

        public BankAccount Withdraw(int accountId, decimal amount)
        {
            if (amount <= 0) throw new InvalidTransferAmountException(amount);

            BankAccount account = _bankRepository.GetById(accountId);

            account.Withdraw(amount);

            return account;
        }

        public IReadOnlyCollection<BankAccount> GetAllAccounts()
        {
            return _bankRepository.GetAll();
        }
    
        public BankAccount GetAccountById(int accountId)
        {
            return _bankRepository.GetById(accountId);
        }
    }
}
