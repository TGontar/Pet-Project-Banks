using Application.Models.Accounts;

namespace Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<Account?> FindAccountByName(string name);
    Task RegisterAccount(string name, string password);
    Task UpdateAccount(long accountId, double money);
}