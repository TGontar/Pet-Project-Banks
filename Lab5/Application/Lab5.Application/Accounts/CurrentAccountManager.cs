using Application.Contracts.Accounts;
using Application.Models.Accounts;

namespace Application.Accounts;

public class CurrentAccountManager : ICurrentAccountService
{
    public Account? Account { get; set; }
}