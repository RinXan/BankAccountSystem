using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Exceptions;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Domain.Repositories;
using BankAccountSystem.Domain.Services;
using BankAccountSystem.Infrastructure.Logger;
using BankAccountSystem.Infrastructure.Repositories;

string filePath = "D:\\practise\\c#\\BankAccountSystem\\Infrastructure\\log.txt";
ILogger logger = new FileLogger(filePath);

try
{
    BankAccount anna = new SavingsAccount(2, "Anna", 100);
    BankAccount masha = new CreditAccount(3, "Masha", 1000);
    BankAccount olga = new SavingsAccount(4, "Olya", 1000000);
    List<BankAccount> bankAccounts = [];

    IAccountRepository bankRepository = new InMemoryAccountRepository(bankAccounts);
    TransferService transferService = new TransferService(bankRepository, logger);

    bankRepository.Add(anna);
    bankRepository.Add(masha);
    bankRepository.Add(olga);

    anna.Deposit(6000);
    masha.Deposit(150000);

    Console.WriteLine(anna.PrintInfo());
    Console.WriteLine(masha.PrintInfo());

    transferService.Transfer(masha.Id, anna.Id, 100000);
    transferService.Transfer(anna.Id, masha.Id, 86000);

    Console.WriteLine(anna.PrintInfo());
    Console.WriteLine(masha.PrintInfo());
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