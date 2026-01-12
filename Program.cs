using BankAccountSystem.Domain.Accounts;
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
    List<BankAccount> bankAccounts = [anna, masha];


    IAccountRepository bankRepository = new InMemoryAccountRepository(bankAccounts);
    TransferService transferService = new TransferService(bankRepository, logger);

    bankRepository.Add(olga);

    anna.Deposit(6000);
    masha.Deposit(150000);

    Console.WriteLine(anna.PrintInfo());
    Console.WriteLine(masha.PrintInfo());

    transferService.Transfer(masha.Id, anna.Id, 50000);

    Console.WriteLine(anna.PrintInfo());
    Console.WriteLine(masha.PrintInfo());
} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    logger.Log(LogLevel.Error, ex.Message);   
}

Console.WriteLine("Operation success");