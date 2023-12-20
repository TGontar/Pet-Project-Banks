using Application.Abstractions.Repositories;
using Application.Models.Histories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public HistoryRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async IAsyncEnumerable<History>? GetAllHistory(long accountId)
    {
        const string sql = """
        select history_id, account_id, operation, money
        from history
        where account_id = :id;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", accountId);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false) is false)
        {
            yield break;
        }

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            yield return new History(
                reader.GetInt64(0),
                reader.GetInt64(1),
                await reader.GetFieldValueAsync<Operation>(2).ConfigureAwait(false),
                reader.GetDouble(3));
        }
    }

    public async Task SaveHistory(long accountId, Operation operation, double money)
    {
        const string sql = """
        insert into history (account_id, operation, money)
        values (:account_id, :operation, :money)
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("account_id", accountId);
        command.AddParameter("operation", operation);
        command.AddParameter("money", money);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}