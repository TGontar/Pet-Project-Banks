namespace Application.Contracts.Accounts;

public abstract record AccountLoginResult
{
    private AccountLoginResult() { }

    public sealed record Success : AccountLoginResult;

    public sealed record AccountNotFound : AccountLoginResult;

    public sealed record IncorrectPassword : AccountLoginResult;
}