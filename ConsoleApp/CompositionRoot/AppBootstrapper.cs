using BankAccountSystem.ConsoleApp.UI;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Infrastructure.Logger;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Infrastructure.Repositories;
using BankAccountSystem.Domain.Services;
using BankAccountSystem.ConsoleApp.Controllers;
using BankAccountSystem.Domain.Parsers;
using BankAccountSystem.Infrastructure.Parsers;

namespace BankAccountSystem.ConsoleApp.CompositionRoot
{
    public static class AppBootstrapper
    {
        static string dataFilePath = "D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\data.txt";
        static string logFilePath = "D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\log.txt";

        public static BankConsoleApp Build()
        {
            ILogger logger = new FileLogger(logFilePath);
            IAccountParser parser = new TextAccountParser();
            IAccountLoader loader = new FileAccountLoader(dataFilePath, parser);

            IEnumerable<BankAccount> accounts = loader.Load();

            IAccountRepository bankRepository = new InMemoryAccountRepository(accounts.ToList());
            //IAccountRepository bankRepositorySql = new SqlAccountRepository(Url);

            AccountService accountService = new AccountService(bankRepository, logger);
            TransferController controller = new TransferController(accountService, logger);
            ConsoleMenu menu = new ConsoleMenu(controller);

            return new BankConsoleApp(menu);
        }
    }
}
