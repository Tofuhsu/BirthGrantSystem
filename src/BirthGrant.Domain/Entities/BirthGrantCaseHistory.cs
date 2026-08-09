namespace BirthGrant.Domain.Entities;

public class BirthGrantCaseHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BirthGrantCaseId { get; set; }
    public BirthGrantCase? BirthGrantCase { get; set; }

    public string ActionType { get; set; } = string.Empty;
    public string ActionDescription { get; set; } = string.Empty;
    public string OperatorName { get; set; } = string.Empty;

    public DateTime OperatedAt { get; set; } = DateTime.Now;
}