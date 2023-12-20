using Application.Abstractions.Repositories;
using Application.Models.Accounts;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Account?> FindAccountByName(string name)
    {
        const string sql = """
        select account_id, account_name, account_password, account_money
        from accounts
        where account_name = :name;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", name);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false) is false)
        {
            return null;
        }

        return new Account(
            Id: reader.GetInt64(0),
            Name: reader.GetString(1),
            Password: reader.GetString(2),
            Money: reader.GetDouble(3));
    }

    public async Task RegisterAccount(string name, string password)
    {
        const string sql = """
        insert into accounts (account_name, account_password, account_money)
        values (:name, :password, 0);
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", name);
        command.AddParameter("password", password);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task UpdateAccount(long accountId, double money)
    {
        const string sql = """
        update accounts
        set account_money = :money
        where account_id = :id
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("money", money);
        command.AddParameter("id", accountId);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}