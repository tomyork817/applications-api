using Application.Dto;
using Domain.Applications;

namespace Application.Mapping;

public static class UnsubmittedApplicationDtoMapper
{
    public static UnsubmittedApplicationDto AsDto(this UnsubmittedApplication application)
        => new UnsubmittedApplicationDto(application.Id,
            application.UserId,
            application.Activity?.Name,
            application.Name,
            application.Description,
            application.Plan);
}