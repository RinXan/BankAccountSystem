[1mdiff --git a/Domain/Accounts/AccountFactory.cs b/Domain/Accounts/AccountFactory.cs[m
[1mindex ae63b1b..a256b17 100644[m
[1m--- a/Domain/Accounts/AccountFactory.cs[m
[1m+++ b/Domain/Accounts/AccountFactory.cs[m
[36m@@ -3,6 +3,7 @@[m [musing System.Collections.Generic;[m
 using System.Linq;[m
 using System.Text;[m
 using System.Threading.Tasks;[m
[32m+[m[32musing BankAccountSystem.Domain.Exceptions;[m
 [m
 namespace BankAccountSystem.Domain.Accounts[m
 {[m
[36m@@ -14,7 +15,7 @@[m [mnamespace BankAccountSystem.Domain.Accounts[m
             {[m
                 "Savings" => new SavingsAccount(id, name, amount),[m
                 "Credit" => new CreditAccount(id, name, amount),[m
[31m-                _ => throw new Exception("Unknown account type")[m
[32m+[m[32m                _ => throw new UnknownAccontTypeException(type)[m
             };[m
             return account;[m
         }[m
[1mdiff --git a/Infrastructure/Repositories/InMemoryAccountRepository.cs b/Infrastructure/Repositories/InMemoryAccountRepository.cs[m
[1mindex f106d86..42f0583 100644[m
[1m--- a/Infrastructure/Repositories/InMemoryAccountRepository.cs[m
[1m+++ b/Infrastructure/Repositories/InMemoryAccountRepository.cs[m
[36m@@ -35,7 +35,7 @@[m [mnamespace BankAccountSystem.Infrastructure.Repositories[m
             return new List<BankAccount>(_bankAccounts);[m
         }[m
 [m
[31m-        public BankAccount? GetById(int id)[m
[32m+[m[32m        public BankAccount GetById(int id)[m
         {[m
             if (id < 1) throw new ArgumentOutOfRangeException(nameof(id));[m
 [m
[1mdiff --git a/Program.cs b/Program.cs[m
[1mindex 8eebc03..3316c63 100644[m
[1m--- a/Program.cs[m
[1m+++ b/Program.cs[m
[36m@@ -51,6 +51,9 @@[m [mcatch (DomainException ex)[m
         case SameAccountTransferException e:[m
             Console.WriteLine($"Transfer to same account is not allowed");[m
             break;[m
[32m+[m[32m        case UnknownAccontTypeException e:[m
[32m+[m[32m            Console.WriteLine($"{e.AccountType} account type does not supported");[m
[32m+[m[32m            break;[m
         default:[m
             Console.WriteLine("Operation error");[m
             break;[m
