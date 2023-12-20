using Application.Models.Histories;

namespace Application.Contracts.Histories;

public interface IHistoryService
{
    void SaveHistory(long accountId, Operation operation, double money);
}