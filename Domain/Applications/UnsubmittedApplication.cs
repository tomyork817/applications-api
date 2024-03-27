namespace Domain.Applications;

public class UnsubmittedApplication : IEquatable<UnsubmittedApplication>
{
    public UnsubmittedApplication(
        Guid id,
        Guid userId,
        ApplicationActivity? activity,
        string? name,
        string? description,
        string? plan)
    {
        Id = id;
        UserId = userId;
        Activity = activity;
        Name = name;
        Description = description;
        Plan = plan;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public ApplicationActivity? Activity { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Plan { get; set; }

    public bool Equals(UnsubmittedApplication? other) => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj) => Equals(obj as UnsubmittedApplication);

    public override int GetHashCode() => Id.GetHashCode();
}