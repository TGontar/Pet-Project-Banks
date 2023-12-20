namespace Application.Contracts.Admins;

public interface IAdminService
{
    AdminLoginResult Login(string username, string password);
    void RegisterAccount(string username, string password);
}