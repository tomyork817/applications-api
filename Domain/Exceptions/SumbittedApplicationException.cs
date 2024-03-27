namespace Domain.Exceptions;

public class SumbittedApplicationException : Exception
{
    protected SumbittedApplicationException(string? message) : base(message) { }

    public static SumbittedApplicationException SubmitApplication()
        => new SumbittedApplicationException($"Not all required fields are filled in application to submit");
}