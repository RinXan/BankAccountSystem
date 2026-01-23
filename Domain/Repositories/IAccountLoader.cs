using BankAccountSystem.Domain.Accounts;

namespace BankAccountSystem.Domain.Repositories
{
    public interface IAccountLoader
    {
        IEnumerable<BankAccount> Load();
    }
}
