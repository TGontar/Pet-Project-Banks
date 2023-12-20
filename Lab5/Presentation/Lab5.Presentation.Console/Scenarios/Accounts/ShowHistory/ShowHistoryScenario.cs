using Application.Contracts.Accounts;
using Application.Models.Histories;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Accounts.ShowHistory;

public class ShowHistoryScenario : IScenario
{
    private readonly IAccountService _accountService;

    public ShowHistoryScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Show Account history";

    public void Run()
    {
        IEnumerable<History> result = _accountService.ShowHistory().ToList();

        if (result.Any())
        {
            foreach (History history in result)
            {
                switch (history.Operation)
                {
                    case Operation.AddMoney:
                        AnsiConsole.WriteLine($"Added {history.Money} dollars");
                        break;
                    case Operation.WithdrawMoney:
                        AnsiConsole.WriteLine($"Withdraw {history.Money} dollars");
                        break;
                }
            }
        }
        else
        {
            AnsiConsole.WriteLine("No history for current user");
        }

        AnsiConsole.Ask<string>("Ok");
    }
}