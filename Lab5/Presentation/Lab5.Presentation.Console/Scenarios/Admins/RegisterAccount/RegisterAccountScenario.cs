using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Admins.RegisterAccount;

public class RegisterAccountScenario : IScenario
{
    private readonly IAdminService _adminService;

    public RegisterAccountScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Register new account";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your account username");
        string password = AnsiConsole.Ask<string>("Enter your account password");

        _adminService.RegisterAccount(username, password);

        string message = $"Registered new account {username} : {password}";

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}