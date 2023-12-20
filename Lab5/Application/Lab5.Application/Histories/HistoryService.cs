using Application.Abstractions.Repositories;
using Application.Contracts.Histories;
using Application.Models.Histories;

namespace Application.Histories;

public class HistoryService : IHistoryService
{
    private readonly IHistoryRepository _historyRepository;

    public HistoryService(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public void SaveHistory(long accountId, Operation operation, double money)
    {
        _historyRepository.SaveHistory(accountId, operation, money);
    }
}