using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using Application.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.Mocks;

public class AccountRepositoryMock : IAccountRepository
{
    private IList<Account> _accounts;

    public AccountRepositoryMock(IList<Account> accounts)
    {
        _accounts = accounts;
    }

    public Task<Account?> FindAccountByName(string name) =>
        Task.FromResult(_accounts.ToList().Find(ac => ac.Name == name));

    public Task RegisterAccount(string name, string password)
    {
        _accounts.Add(new Account(_accounts[^1].Id + 1, name, password, 0));
        return Task.CompletedTask;
    }

    public Task UpdateAccount(long accountId, double money)
    {
        Account? account = _accounts.ToList().Find(ac => ac.Id == accountId);

        if (account is null)
        {
            return Task.CompletedTask;
        }

        account = new Account(account.Id, account.Name, account.Password, money);
        return Task.CompletedTask;
    }
}