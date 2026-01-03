using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Logger;
using BankAccountSystem.Domain.Services;

BankAccount anna = new SavingsAccount(2, "Anna", 20000);
BankAccount masha = new CreditAccount(3, "Masha", 100);

ILogger logger = new ConsoleLogger();
TransferService transferService = new TransferService(logger);

anna.Deposit(90000);
masha.Deposit(100);

Console.WriteLine(anna.PrintInfo());
Console.WriteLine(masha.PrintInfo());

transferService.Transfer(anna, masha, 5000);

Console.WriteLine(anna.PrintInfo());
Console.WriteLine(masha.PrintInfo());
