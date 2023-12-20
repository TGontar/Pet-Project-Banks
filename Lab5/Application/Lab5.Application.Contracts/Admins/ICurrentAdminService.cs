using Application.Models.Admins;

namespace Application.Contracts.Admins;

public interface ICurrentAdminService
{
    Admin? Admin { get; }
}