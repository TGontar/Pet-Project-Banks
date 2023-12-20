using Application.Abstractions.Repositories;
using Application.Models.Admins;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Admin?> FindAdminByUserName(string userName)
    {
        const string sql = """
        select admin_id, admin_name, admin_password
        from admins
        where admin_name = :name;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", userName);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false) is false)
        {
            return null;
        }

        return new Admin(
            Id: reader.GetInt64(0),
            Username: reader.GetString(1),
            Password: reader.GetString(2));
    }

    public async Task UpdateAdminPassword(long adminId, string password)
    {
        const string sql = """
        update admins
        set password = :password
        where admin_id = :id
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("password", password);
        command.AddParameter("admin_id", adminId);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}