using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Admins;

namespace Presentation.Console.Scenarios.Admins.RegisterAccount;

public class RegisterAccountScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAdminService _currentAdminService;

    public RegisterAccountScenarioProvider(
        IAdminService adminService,
        ICurrentAdminService currentAdminService)
    {
        _adminService = adminService;
        _currentAdminService = currentAdminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdminService.Admin is null)
        {
            scenario = null;
            return false;
        }

        scenario = new RegisterAccountScenario(_adminService);
        return true;
    }
}