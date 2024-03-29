namespace Domain.Applications;

public class UnsubmittedApplication : IEquatable<UnsubmittedApplication>
{
    public UnsubmittedApplication() { }
        
    public UnsubmittedApplication(
        Guid id,
        Guid userId,
        ApplicationActivity? activity,
        string? name,
        string? description,
        string? plan, 
        DateTime createdDateTime)
    {
        Id = id;
        UserId = userId;
        Activity = activity;
        Name = name;
        Description = description;
        Plan = plan;
        CreatedDateTime = createdDateTime;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public ApplicationActivity? Activity { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Plan { get; set; }
    public DateTime CreatedDateTime { get; }

    public bool Equals(UnsubmittedApplication? other) => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj) => Equals(obj as UnsubmittedApplication);

    public override int GetHashCode() => Id.GetHashCode();
}