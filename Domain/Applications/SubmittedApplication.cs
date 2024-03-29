namespace Domain.Applications;

public class SubmittedApplication : IEquatable<SubmittedApplication>
{
    public SubmittedApplication() { }
    
    public SubmittedApplication(
        Guid id,
        Guid userId,
        string name,
        string? description,
        string plan, 
        DateTime createdDateTime)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Plan = plan;
        CreatedDateTime = createdDateTime;
        Description = description;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public ApplicationActivity? Activity { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Plan { get; set; }
    public DateTime CreatedDateTime { get; }

    public bool Equals(SubmittedApplication? other) => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj) => Equals(obj as SubmittedApplication);

    public override int GetHashCode() => Id.GetHashCode();
}