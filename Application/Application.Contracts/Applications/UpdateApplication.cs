using Application.Dto;
using MediatR;

namespace Application.Contracts.Applications;

public static class UpdateApplication
{
    public record struct Command(Guid Id, string? Activity, string? Name, string? Description, string? Outline) : IRequest<Response>;
        
    public abstract record Response;

    public sealed record Success(UnsubmittedApplicationDto Application) : Response;

    public sealed record Failed(string Error) : Response;
}