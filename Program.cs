using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Services;

BankAccount anna = new SavingsAccount(2, "Anna", 20000);
BankAccount masha = new CreditAccount(3, "Masha", 100);

TransferService transferService = new TransferService();

anna.Deposit(90000);
masha.Deposit(100);

transferService.Transfer(anna, masha, 5000);

Console.WriteLine(anna.PrintInfo());
Console.WriteLine(masha.PrintInfo());
