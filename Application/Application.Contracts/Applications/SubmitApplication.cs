using MediatR;

namespace Application.Contracts.Applications;

public static class SubmitApplication
{
    public record struct Command(Guid Id) : IRequest<Response>;
        
    public abstract record Response;

    public sealed record Success(string Message) : Response;

    public sealed record Failed(string Error) : Response;
}