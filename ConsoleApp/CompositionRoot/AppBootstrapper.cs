using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.ConsoleApp.UI;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Infrastructure.Logger;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Infrastructure.Repositories;
using BankAccountSystem.Domain.Services;
using BankAccountSystem.ConsoleApp.Controllers;

namespace BankAccountSystem.ConsoleApp.CompositionRoot
{
    public static class AppBootstrapper
    {
        public static BankConsoleApp Build()
        {
            ILogger logger = new FileLogger("D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\log.txt");

            var accounts = new List<BankAccount>();
            IAccountRepository repository = new InMemoryAccountRepository(accounts);

            var transferService = new TransferService(repository, logger);
            var controller = new TransferController(transferService);
            var menu = new ConsoleMenu(controller);

            return new BankConsoleApp(menu);
        }
    }
}
