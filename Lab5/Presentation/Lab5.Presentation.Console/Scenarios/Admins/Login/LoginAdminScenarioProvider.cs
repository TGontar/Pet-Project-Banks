using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Accounts;
using Application.Contracts.Admins;

namespace Presentation.Console.Scenarios.Admins.Login;

public class LoginAdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAdminService _currentAdminService;
    private readonly ICurrentAccountService _currentAccountService;

    public LoginAdminScenarioProvider(
        IAdminService adminService,
        ICurrentAdminService currentAdminService,
        ICurrentAccountService currentAccountService)
    {
        _adminService = adminService;
        _currentAdminService = currentAdminService;
        _currentAccountService = currentAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdminService.Admin is not null || _currentAccountService.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginAdminScenario(_adminService);
        return true;
    }
}