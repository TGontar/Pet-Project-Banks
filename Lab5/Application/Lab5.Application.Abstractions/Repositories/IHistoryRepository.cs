using Application.Models.Histories;

namespace Application.Abstractions.Repositories;

public interface IHistoryRepository
{
    IAsyncEnumerable<History>? GetAllHistory(long accountId);
    Task SaveHistory(long accountId, Operation operation, double money);
}