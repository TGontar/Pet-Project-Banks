using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;

namespace Presentation.Console.Scenarios.Accounts.ChangeBalance;

public class WithdrawMoneyScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;

    public WithdrawMoneyScenarioProvider(
        IAccountService accountService,
        ICurrentAccountService currentAccountService)
    {
        _accountService = accountService;
        _currentAccountService = currentAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is null)
        {
            scenario = null;
            return false;
        }

        scenario = new WithdrawMoneyScenario(_accountService);
        return true;
    }
}