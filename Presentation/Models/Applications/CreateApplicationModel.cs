namespace Presentation.Models.Applications;

public record CreateApplicationModel(Guid? Author, string? Activity, string? Name, string? Description, string? Outline);