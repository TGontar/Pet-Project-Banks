namespace Application.Contracts.Accounts;

public abstract record WithdrawMoneyResult
{
    private WithdrawMoneyResult() { }

    public sealed record Success : WithdrawMoneyResult;

    public sealed record NotEnoughMoney : WithdrawMoneyResult;

    public sealed record NotAuthorized : WithdrawMoneyResult;
}