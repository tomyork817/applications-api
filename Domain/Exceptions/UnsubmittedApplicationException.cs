namespace Domain.Exceptions;

public class UnsubmittedApplicationException : DomainException
{
    protected UnsubmittedApplicationException(string? message) : base(message) { }

    public static UnsubmittedApplicationException CreateApplication()
        => new UnsubmittedApplicationException($"Not all required fields are filled in unsubmitted application");
}