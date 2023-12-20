using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using Application.Models.Histories;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.Mocks;

public class HistoryRepositoryMock : IHistoryRepository
{
    public IAsyncEnumerable<History>? GetAllHistory(long accountId)
    {
        return null;
    }

    public Task SaveHistory(long accountId, Operation operation, double money)
    {
        return Task.CompletedTask;
    }
}