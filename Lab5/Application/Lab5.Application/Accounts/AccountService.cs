using Application.Abstractions.Repositories;
using Application.Contracts.Accounts;
using Application.Contracts.Histories;
using Application.Models.Accounts;
using Application.Models.Histories;

namespace Application.Accounts;

public class AccountService : IAccountService
{
    private readonly IHistoryService _historyService;
    private readonly CurrentAccountManager _currentAccountManager;
    private readonly IAccountRepository _accountRepository;
    private readonly IHistoryRepository _historyRepository;

    public AccountService(
        IHistoryService historyService,
        CurrentAccountManager currentAccountManager,
        IAccountRepository accountRepository,
        IHistoryRepository historyRepository)
    {
        _historyService = historyService;
        _currentAccountManager = currentAccountManager;
        _accountRepository = accountRepository;
        _historyRepository = historyRepository;
    }

    public AccountLoginResult Login(string username, string password)
    {
        Account? account = _accountRepository.FindAccountByName(username).Result;

        if (account is null)
        {
            return new AccountLoginResult.AccountNotFound();
        }

        if (account.Password != password)
        {
            return new AccountLoginResult.IncorrectPassword();
        }

        _currentAccountManager.Account = account;
        return new AccountLoginResult.Success();
    }

    public double ShowMoney()
    {
        return _currentAccountManager.Account?.Money ?? 0;
    }

    public AddMoneyResult AddMoney(double money)
    {
        if (_currentAccountManager.Account is null)
        {
            return new AddMoneyResult.NotAuthorized();
        }

        _accountRepository.UpdateAccount(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Money + money);

        _currentAccountManager.Account = new Account(
            _currentAccountManager.Account.Id,
            _currentAccountManager.Account.Name,
            _currentAccountManager.Account.Password,
            _currentAccountManager.Account.Money + money);

        _historyService.SaveHistory(_currentAccountManager.Account.Id, Operation.AddMoney, money);
        return new AddMoneyResult.Success();
    }

    public WithdrawMoneyResult WithdrawMoney(double money)
    {
        if (_currentAccountManager.Account is null)
        {
            return new WithdrawMoneyResult.NotAuthorized();
        }

        if (_currentAccountManager.Account.Money < money)
        {
            return new WithdrawMoneyResult.NotEnoughMoney();
        }
        else
        {
            _accountRepository.UpdateAccount(
                _currentAccountManager.Account.Id,
                _currentAccountManager.Account.Money - money);

            _currentAccountManager.Account = new Account(
                _currentAccountManager.Account.Id,
                _currentAccountManager.Account.Name,
                _currentAccountManager.Account.Password,
                _currentAccountManager.Account.Money - money);

            _historyService.SaveHistory(_currentAccountManager.Account.Id, Operation.WithdrawMoney, money);
            return new WithdrawMoneyResult.Success();
        }
    }

    public IEnumerable<History> ShowHistory()
    {
        if (_currentAccountManager.Account is null)
        {
            return new List<History>();
        }

        IAsyncEnumerable<History>? history = _historyRepository.GetAllHistory(_currentAccountManager.Account.Id);

        if (history is null)
        {
            return new List<History>();
        }

        return history.ToBlockingEnumerable();
    }
}