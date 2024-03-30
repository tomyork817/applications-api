using Application.Dto;
using MediatR;

namespace Application.Contracts.Applications;

public static class GetUnsubmittedApplications
{
    public record struct Command(DateTime Time) : IRequest<Response>;
        
    public abstract record Response;

    public sealed record Success(IEnumerable<ApplicationDto> Applications) : Response;

    public sealed record Failed(string Error) : Response;
}