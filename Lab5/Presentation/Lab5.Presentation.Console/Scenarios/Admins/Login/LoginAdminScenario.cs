using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Admins.Login;

public class LoginAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public LoginAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Login Admin";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");

        AdminLoginResult result = _adminService.Login(username, password);

        string message = result switch
        {
            AdminLoginResult.Success => "Successful login",
            AdminLoginResult.AdminNotFound => "Admin not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}