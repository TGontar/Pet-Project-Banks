using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.ShowBalance;

public class ShowBalanceScenario : IScenario
{
    private readonly IAccountService _accountService;

    public ShowBalanceScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Show Account balance";

    public void Run()
    {
        double result = _accountService.ShowMoney();

        AnsiConsole.WriteLine($"{result}");
        AnsiConsole.Ask<string>("Ok");
    }
}