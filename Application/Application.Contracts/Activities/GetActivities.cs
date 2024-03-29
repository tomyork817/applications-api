using Application.Dto;
using MediatR;

namespace Application.Contracts.Activities;

public static class GetActivities
{
    public record struct Command() : IRequest<Response>;
        
    public record struct Response(IEnumerable<ActivityDto> Activities);
}