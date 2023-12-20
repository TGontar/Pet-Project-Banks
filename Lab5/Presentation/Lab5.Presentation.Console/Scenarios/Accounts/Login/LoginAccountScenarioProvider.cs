using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;
using Application.Contracts.Admins;

namespace Presentation.Console.Scenarios.Accounts.Login;

public class LoginAccountScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ICurrentAdminService _currentAdminService;

    public LoginAccountScenarioProvider(
        IAccountService accountService,
        ICurrentAccountService currentAccountService,
        ICurrentAdminService currentAdminService)
    {
        _accountService = accountService;
        _currentAccountService = currentAccountService;
        _currentAdminService = currentAdminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is not null || _currentAdminService.Admin is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAccountScenario(_accountService);
        return true;
    }
}