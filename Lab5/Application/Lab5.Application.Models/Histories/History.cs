namespace Application.Models.Histories;

public record History(long Id, long AccountId, Operation Operation, double Money);