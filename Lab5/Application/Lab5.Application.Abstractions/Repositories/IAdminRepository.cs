using Application.Models.Admins;

namespace Application.Abstractions.Repositories;

public interface IAdminRepository
{
    Task<Admin?> FindAdminByUserName(string userName);
    Task UpdateAdminPassword(long adminId, string password);
}