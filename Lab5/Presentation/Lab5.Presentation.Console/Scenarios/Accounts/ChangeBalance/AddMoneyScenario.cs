using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.ChangeBalance;

public class AddMoneyScenario : IScenario
{
    private readonly IAccountService _accountService;

    public AddMoneyScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Add money to Account";

    public void Run()
    {
        double money = AnsiConsole.Ask<double>("How much money you want to add?");

        AddMoneyResult result = _accountService.AddMoney(money);

        string message = result switch
        {
            AddMoneyResult.Success => "Successful adding money",
            AddMoneyResult.NotAuthorized => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}