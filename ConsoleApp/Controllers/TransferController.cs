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
        private readonly TransferService _transferService;
        private readonly ILogger _logger;

        public TransferController(TransferService transferService, ILogger logger)
        {
            _transferService = transferService;
            _logger = logger;
        }
        public void Transfer()
        {
            try
            {
                int from = ConsoleInput.ReadInt("From account ID: ");
                int to = ConsoleInput.ReadInt("To account ID: ");
                decimal amount = ConsoleInput.ReadDecimal("Amount: ");

                _transferService.Transfer(from, to, amount);

                Console.WriteLine("Transfer completed");
            }
            catch (DomainException ex)
            {
                _logger.Log(LogLevel.Error, $"{ex.GetType().Name}: {ex.Message}");

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

                var account = _transferService.GetAccountById(accountId);

                Console.WriteLine("\n*** Account details ***");
                Console.WriteLine($"ID: {account.Id}");
                Console.WriteLine($"Owner: {account.Name}");
                Console.WriteLine($"Type: {account.GetType().Name}");
                Console.WriteLine($"Balance: {account.Balance}");
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
                var accounts = _transferService.GetAllAccounts();

                Console.WriteLine("*** Accounts ***");

                foreach ( var account in accounts )
                {
                    Console.WriteLine($"ID: {account.Id} | NAME: {account.Name} | Balance: {account.Balance}");
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
            Console.WriteLine("Not implemented yet. Press any key...");
            Console.ReadKey();
        }
    }
}
