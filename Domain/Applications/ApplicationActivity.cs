namespace Domain.Applications;

public class ApplicationActivity : IEquatable<ApplicationActivity>
{
    public ApplicationActivity() { }

    public ApplicationActivity(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }

    public bool Equals(ApplicationActivity? other) => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj) => Equals(obj as ApplicationActivity);

    public override int GetHashCode() => Id.GetHashCode();
}