using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.ChangeBalance;

public class WithdrawMoneyScenario : IScenario
{
    private readonly IAccountService _accountService;

    public WithdrawMoneyScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Withdraw money from Account";

    public void Run()
    {
        double money = AnsiConsole.Ask<double>("How much money you want to withdraw?");

        WithdrawMoneyResult result = _accountService.WithdrawMoney(money);

        string message = result switch
        {
            WithdrawMoneyResult.Success => "Successful withdrawing money",
            WithdrawMoneyResult.NotEnoughMoney => "Not enough money on account",
            WithdrawMoneyResult.NotAuthorized => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}