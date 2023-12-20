namespace Application.Contracts.Admins;

public abstract record AdminLoginResult
{
    private AdminLoginResult() { }

    public sealed record Success : AdminLoginResult;

    public sealed record AdminNotFound : AdminLoginResult;

    public sealed record IncorrectPassword : AdminLoginResult;
}