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
        
        public void CreateSavingAccount(int id, string name, decimal balance)
        {
            var account = new SavingsAccount(id, name, balance);
            
            _bankRepository.Add(account);

            _logger.Log(LogLevel.Info, $"Savings account {account.Id} created");
        }
      
        public void CreateCreditAccount(int id, string name, decimal balance)
        {
            var account = new CreditAccount(id, name, balance);
            
            _bankRepository.Add(account);

            _logger.Log(LogLevel.Info, $"Credit account {account.Id} created");
        }
        
        public void DeleteAccount(int accountId)
        {
            _bankRepository.Delete(accountId);

            _logger.Log(LogLevel.Info, $"Account {accountId} deleted");
        }

        public void Transfer(int fromAccountId, int toAccountId, decimal money)
        {
            _bankRepository.Transfer(fromAccountId, toAccountId, money);
            _logger.Log(LogLevel.Info, $"Transfer {money} from {fromAccountId} to {toAccountId}");
        }
    
        public void Deposit(int accountId, decimal amount)
        {
            _bankRepository.Deposit(accountId, amount);
            _logger.Log(LogLevel.Info, $"Deposit {amount} to {accountId}");
        }

        public void Withdraw(int accountId, decimal amount)
        {
            _bankRepository.Withdraw(accountId, amount);
            _logger.Log(LogLevel.Info, $"Withdraw {amount} from {accountId}");
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
