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
                    case UnknownAccontTypeException e:
                        Console.WriteLine($"{e.AccountType} account type does not supported");
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
        }

        internal void ShowAccountsDetails()
        {
            throw new NotImplementedException();
        }

        internal void ShowAllAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
