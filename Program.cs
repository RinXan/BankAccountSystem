using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Exceptions;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Domain.Parsers;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Domain.Services;
using BankAccountSystem.Infrastructure.Logger;
using BankAccountSystem.Infrastructure.Parsers;
using BankAccountSystem.Infrastructure.Repositories;

string logFilePath = "D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\log.txt";
string dataFilePath = "D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\data.txt";
ILogger logger = new FileLogger(logFilePath);

try
{
    IAccountLoader loader = new FileAccountLoader(dataFilePath);
    IAccountRepository bankRepository = new InMemoryAccountRepository(new List<BankAccount>());

    IEnumerable<string> accountsInfo = loader.Load();

    IAccountParser parser = new TextAccountParser();

    TransferService transferService = new TransferService(bankRepository, logger);
    
    

    Console.WriteLine("Operation success");
} 
catch (DomainException ex)
{
    logger.Log(LogLevel.Error, $"{ex.GetType().Name}: {ex.Message}");
    
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
    logger.Log(LogLevel.Fatal, $"{ex.GetType().Name}: {ex.ToString()}");   
    Console.WriteLine("System error");
}