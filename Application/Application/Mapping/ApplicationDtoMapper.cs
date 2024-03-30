using Application.Dto;
using Domain.Applications;

namespace Application.Mapping;

public static class ApplicationDtoMapper
{
    public static ApplicationDto AsDto(this UnsubmittedApplication application)
        => new ApplicationDto(application.Id,
            application.UserId,
            application.Activity?.Name,
            application.Name,
            application.Description,
            application.Plan);
    
    public static ApplicationDto AsDto(this SubmittedApplication application)
        => new ApplicationDto(application.Id,
            application.UserId,
            application.Activity.Name,
            application.Name,
            application.Description,
            application.Plan);
}