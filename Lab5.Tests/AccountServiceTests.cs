using System.Collections.Generic;
using Application.Accounts;
using Application.Contracts.Accounts;
using Application.Histories;
using Application.Models.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class AccountServiceTests
{
    [Fact]
    public void AddMoney_ShouldSuccess_WhenAccountExists()
    {
        // Arrange
        var account = new Account(1, "test", "123", 0);
        var manager = new CurrentAccountManager();
        manager.Account = account;

        var accounts = new List<Account>()
        {
            account,
        };

        var service = new AccountService(
            new HistoryService(new HistoryRepositoryMock()),
            manager,
            new AccountRepositoryMock(accounts),
            new HistoryRepositoryMock());

        // Act
        AddMoneyResult actual = service.AddMoney(100);

        // Assert
        Assert.IsType<AddMoneyResult.Success>(actual);
    }

    [Fact]
    public void WithdrawMoney_ShouldSuccess_WhenEnoughMoney()
    {
        // Arrange
        var account = new Account(1, "test", "123", 100);
        var manager = new CurrentAccountManager();
        manager.Account = account;

        var accounts = new List<Account>()
        {
            account,
        };

        var service = new AccountService(
            new HistoryService(new HistoryRepositoryMock()),
            manager,
            new AccountRepositoryMock(accounts),
            new HistoryRepositoryMock());

        // Act
        WithdrawMoneyResult actual = service.WithdrawMoney(50);

        // Assert
        Assert.IsType<WithdrawMoneyResult.Success>(actual);
    }

    [Fact]
    public void WithdrawMoney_ShouldFail_WhenNotEnoughMoney()
    {
        // Arrange
        var account = new Account(1, "test", "123", 100);
        var manager = new CurrentAccountManager();
        manager.Account = account;

        var accounts = new List<Account>()
        {
            account,
        };

        var service = new AccountService(
            new HistoryService(new HistoryRepositoryMock()),
            manager,
            new AccountRepositoryMock(accounts),
            new HistoryRepositoryMock());

        // Act
        WithdrawMoneyResult actual = service.WithdrawMoney(150);

        // Assert
        Assert.IsType<WithdrawMoneyResult.NotEnoughMoney>(actual);
    }
}