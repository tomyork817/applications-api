using System.Diagnostics;

namespace Application.Dto;

public record UnsubmittedApplicationDto(Guid Id, Guid Author, string? Activity, string? Name, string? Description, string? Outline);