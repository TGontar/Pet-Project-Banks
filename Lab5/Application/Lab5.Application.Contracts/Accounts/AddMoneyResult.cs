namespace Application.Contracts.Accounts;

public abstract record AddMoneyResult
{
    private AddMoneyResult() { }

    public sealed record Success : AddMoneyResult;

    public sealed record NotAuthorized : AddMoneyResult;
}