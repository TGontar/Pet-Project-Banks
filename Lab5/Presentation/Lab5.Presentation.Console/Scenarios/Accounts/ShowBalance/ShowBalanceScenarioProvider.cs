using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;

namespace Presentation.Console.Scenarios.Accounts.ShowBalance;

public class ShowBalanceScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;

    public ShowBalanceScenarioProvider(
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

        scenario = new ShowBalanceScenario(_accountService);
        return true;
    }
}