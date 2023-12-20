using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.Login;

public class LoginAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    public LoginAccountScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Login Account";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");

        AccountLoginResult result = _accountService.Login(username, password);

        string message = result switch
        {
            AccountLoginResult.Success => "Successful login",
            AccountLoginResult.AccountNotFound => "Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}