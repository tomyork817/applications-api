using Domain.Applications;
using FluentValidation;

namespace Domain.Validators;

public class UnsubmittedApplicationValidator : AbstractValidator<UnsubmittedApplication>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 300;
    private const int MaxPlanLength = 1000;
    
    public UnsubmittedApplicationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.UserId)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .MaximumLength(MaxNameLength);
        
        RuleFor(x => x.Description)
            .MaximumLength(MaxDescriptionLength);
        
        RuleFor(x => x.Plan)
            .MaximumLength(MaxPlanLength);

        RuleFor(x => x)
            .Must(x => x.Name is not null || x.Description is not null || x.Plan is not null)
            .WithMessage("Not all required fields are filled in application to submit");
        
        RuleFor(x => x.CreatedDateTime)
            .NotEmpty();
    }
}