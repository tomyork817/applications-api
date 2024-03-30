using Application.Dto;
using MediatR;

namespace Application.Contracts.Users;

public static class GetUserApplication
{
    public record struct Command(Guid UserId) : IRequest<Response>;
        
    public abstract record Response;

    public sealed record Success(ApplicationDto Application) : Response;

    public sealed record Failed(string Error) : Response;
}