using BankAccountSystem.ConsoleApp.UI;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Infrastructure.Logger;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Infrastructure.Repositories;
using BankAccountSystem.Domain.Services;
using BankAccountSystem.ConsoleApp.Controllers;
using BankAccountSystem.Infrastructure.Persistence;

namespace BankAccountSystem.ConsoleApp.CompositionRoot
{
    public static class AppBootstrapper
    {

        public static BankConsoleApp Build()
        {
            string logFilePath = "D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\log.txt";
            string dbPath = "D:\\practise\\c#\\BankAccountSystem\\ConsoleApp\\Data\\bank.db";
            string dbUrl = $"Data Source={dbPath}";

            ILogger logger = new FileLogger(logFilePath);

            DbInitializer.Initialize(dbUrl);

            IAccountRepository sqlBankRepository = new SqlAccountRepository(dbUrl);

            AccountSeeder.Seed(sqlBankRepository);

            AccountService accountService = new AccountService(sqlBankRepository, logger);
            TransferController controller = new TransferController(accountService, logger);
            ConsoleMenu menu = new ConsoleMenu(controller);

            return new BankConsoleApp(menu);
        }
    }
}
