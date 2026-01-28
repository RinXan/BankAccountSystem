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
            string logFilePath = Path.Combine(AppContext.BaseDirectory, "log.txt");
            string dataDir = Path.Combine(AppContext.BaseDirectory, "Data");
            
            Directory.CreateDirectory(dataDir);

            string dbPath = Path.Combine(dataDir, "bank.db");
            string dbUrl = $"Data Source={dbPath}";

            ILogger logger = new FileLogger(logFilePath);

            DbInitializer.Initialize(dbUrl);

            IAccountRepository sqlBankRepository = new SqlAccountRepository(dbUrl);

            AccountService accountService = new AccountService(sqlBankRepository, logger);
            TransferController controller = new TransferController(accountService, logger);
            ConsoleMenu menu = new ConsoleMenu(controller);

            return new BankConsoleApp(menu);
        }
    }
}
