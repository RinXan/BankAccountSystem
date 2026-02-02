using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.ConsoleApp.UI;
using BankAccountSystem.Domain.Exceptions;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Domain.Services;

namespace BankAccountSystem.ConsoleApp.Controllers
{
    public class TransferController
    {
        private readonly AccountService _accountService;
        private readonly ILogger _logger;

        public TransferController(AccountService accountService, ILogger logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        public void Transfer()
        {
            try
            {
                int from = ConsoleInput.ReadInt("From account ID: ");
                int to = ConsoleInput.ReadInt("To account ID: ");
                decimal amount = ConsoleInput.ReadDecimal("Amount: ");

                _accountService.Transfer(from, to, amount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Transfer completed");
                Console.ResetColor();
            }
            catch (DomainException ex)
            {
                _logger.Log(LogLevel.Error, $"{ex.GetType().Name}: {ex.Message}");

                ShowDomainException(ex);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Fatal, $"{ex.GetType().Name}: {ex.ToString()}");
                Console.WriteLine("System error");
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        public void ShowAccountsDetails()
        {
            try
            {
                int accountId = ConsoleInput.ReadInt("Account ID: ");

                var account = _accountService.GetAccountById(accountId);
                string type = account.GetType().Name;
                string accountParametr = type == "SavingsAccount" ? "100$" : "10000$";

                Console.WriteLine("\n*** ACCOUNT DETAILS ***");
                Console.WriteLine($"ID: {account.Id}");
                Console.WriteLine($"Name: {account.Name}");
                Console.WriteLine($"Type: {type} - {accountParametr}");
                Console.WriteLine($"Balance: {account.Balance}$");
            }
            catch (AccountNotFoundException e)
            {
                _logger.Log(LogLevel.Warn, e.Message);
                Console.WriteLine($"Account with id {e.AccountId} not found");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.ToString());
                Console.WriteLine("Error to load account");
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        public void ShowAllAccounts()
        {
            try
            {
                var accounts = _accountService.GetAllAccounts();

                Console.WriteLine("*** ACCOUNTS ***");

                foreach ( var account in accounts )
                {
                    Console.WriteLine($"ID: {account.Id} | NAME: {account.Name} | Balance: {account.Balance}$");
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.ToString());
                Console.WriteLine("Error to load accounts");
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    
        public void Deposit()
        {
            try
            {
                Console.WriteLine("*** DEPOSIT ***");
                
                int accountId = ConsoleInput.ReadInt("Enter account ID: ");
                decimal amount = ConsoleInput.ReadDecimal("Enter amount: ");

                _accountService.Deposit(accountId, amount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDeposit completed successfully!");
                Console.ResetColor();
            }
            catch (DomainException ex)
            {
                _logger.Log(LogLevel.Warn, ex.Message);
                ShowDomainException(ex);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Fatal, e.ToString());
                Console.WriteLine("System error");
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    
        public void Withdraw()
        {
            try
            {
                Console.WriteLine("*** WITHDRAW ***");

                int accountId = ConsoleInput.ReadInt("Enter account ID: ");
                decimal amount = ConsoleInput.ReadDecimal("Enter amount: ");

                _accountService.Withdraw(accountId, amount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nWithdraw completed successfully!");
                Console.ResetColor();
            }
            catch (DomainException ex)
            {
                _logger.Log(LogLevel.Warn, ex.Message);
                ShowDomainException(ex);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Fatal, e.ToString());
                Console.WriteLine("System error");
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        public void CreateAccount()
        {
            int id = ConsoleInput.ReadInt("Enter account id: ");
            string name = ConsoleInput.ReadString("Enter user name : ");
            decimal balance = ConsoleInput.ReadDecimal("Enter account balance: ");

            Console.WriteLine("\n1. Credit account");
            Console.WriteLine("2. Savings account");

            int type = ConsoleInput.ReadInt("\nChoose account type: ");
            
            if (type == 1)
            {
                _accountService.CreateCreditAccount(id, name, balance);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAccount created successfully!");
                Console.ResetColor();
            }
            else if (type == 2)
            {
                _accountService.CreateSavingAccount(id, name, balance);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAccount created successfully!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Unknown account type");
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        public void DeleteAccount()
        {
            int accountId = ConsoleInput.ReadInt("Enter account id: ");

            _accountService.DeleteAccount(accountId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAccount deleted successfully!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
        private void ShowDomainException(DomainException ex)
        {
            switch (ex)
            {
                case AccountNotFoundException e:
                    Console.WriteLine($"Account with ID {e.AccountId} not found");
                    break;
                case InvalidTransferAmountException e:
                    Console.WriteLine($"Incorrect transfer amount: {e.Amount}");
                    break;
                case NotEnoughMoneyException e:
                    Console.WriteLine($"Account {e.AccountId}'s balance {e.Balance}. Requested money: {e.RequestedMoney}");
                    break;
                case SameAccountTransferException e:
                    Console.WriteLine($"Transfer to same account is not allowed");
                    break;
                default:
                    Console.WriteLine("Operation error");
                    break;
            }
        }

    }
}
