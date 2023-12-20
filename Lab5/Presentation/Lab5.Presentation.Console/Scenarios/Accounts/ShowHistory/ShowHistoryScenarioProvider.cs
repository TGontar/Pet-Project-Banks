using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;

namespace Presentation.Console.Scenarios.Accounts.ShowHistory;

public class ShowHistoryScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;

    public ShowHistoryScenarioProvider(
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

        scenario = new ShowHistoryScenario(_accountService);
        return true;
    }
}