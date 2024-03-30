using Application.Abstractions.DataAccess;
using Domain.Applications;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static Application.Contracts.Applications.SubmitApplication;

namespace Application.Applications;

public class SubmitApplicationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IValidator<SubmittedApplication> _validator;

    public SubmitApplicationHandler(IDatabaseContext databaseContext, IValidator<SubmittedApplication> validator)
    {
        _databaseContext = databaseContext;
        _validator = validator;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var unsubmittedApplication = await _databaseContext.UnsubmittedApplications
            .Include(a => a.Activity)
            .Where(a => a.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (unsubmittedApplication is null)
        {
            return new Failed("Application not found");
        }

        var submittedApplication = new SubmittedApplication(
            unsubmittedApplication.Id,
            unsubmittedApplication.UserId,
            unsubmittedApplication.Activity,
            unsubmittedApplication.Name,
            unsubmittedApplication.Description,
            unsubmittedApplication.Plan,
            DateTime.Now.ToUniversalTime());

        var validationResult = await _validator.ValidateAsync(submittedApplication, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Failed(Strings.Join(validationResult.Errors.Select(e => e.ErrorMessage).ToArray(), "\n"));
        }

        await using var transaction = await _databaseContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _databaseContext.UnsubmittedApplications.Remove(unsubmittedApplication);
            _databaseContext.SubmittedApplications.Add(submittedApplication);

            await _databaseContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new Failed("Can't submit application");
        }

        return new Success("Ok");
    }
}