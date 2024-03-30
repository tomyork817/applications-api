using Application.Dto;
using MediatR;

namespace Application.Contracts.Applications;

public static class GetApplication
{
    public record struct Command(Guid Id) : IRequest<Response>;
        
    public abstract record Response;

    public sealed record Success(ApplicationDto Application) : Response;

    public sealed record Failed(string Error) : Response;
}