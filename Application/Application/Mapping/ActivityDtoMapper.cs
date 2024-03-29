using Application.Dto;
using Domain.Applications;

namespace Application.Mapping;

public static class ActivityDtoMapper
{
    public static ActivityDto AsDto(this ApplicationActivity activity)
        => new ActivityDto(activity.Name, activity.Description);
}