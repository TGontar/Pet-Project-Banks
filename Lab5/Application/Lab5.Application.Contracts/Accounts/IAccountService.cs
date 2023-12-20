using Application.Models.Histories;

namespace Application.Contracts.Accounts;

public interface IAccountService
{
    AccountLoginResult Login(string username, string password);
    double ShowMoney();
    AddMoneyResult AddMoney(double money);
    WithdrawMoneyResult WithdrawMoney(double money);
    IEnumerable<History> ShowHistory();
}