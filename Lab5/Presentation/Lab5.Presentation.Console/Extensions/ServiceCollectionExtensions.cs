using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenarios.Accounts.ChangeBalance;
using Presentation.Console.Scenarios.Accounts.Login;
using Presentation.Console.Scenarios.Accounts.ShowBalance;
using Presentation.Console.Scenarios.Accounts.ShowHistory;
using Presentation.Console.Scenarios.Admins.Login;
using Presentation.Console.Scenarios.Admins.RegisterAccount;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, AddMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowHistoryScenarioProvider>();

        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, RegisterAccountScenarioProvider>();

        return collection;
    }
}