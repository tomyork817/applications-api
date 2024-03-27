using Domain.Applications;
using Domain.Exceptions;

namespace Domain.Validators;

public static class ApplicationValidator
{
    public static void ValidateUnsubmittedApplication(UnsubmittedApplication application)
    {
        if (application.Activity is null && application.Description is null && application.Plan is null &&
            application.Name is null)
        {
            throw UnsubmittedApplicationException.CreateApplication();
        }
    }

    public static void ValidateSubmittedApplication(UnsubmittedApplication application)
    {
        if (application.Activity is not null && application.Plan is not null &&
            application.Name is not null)
        {
            throw SumbittedApplicationException.SubmitApplication();
        }
    }
}