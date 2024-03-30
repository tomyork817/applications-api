using Domain.Applications;
using FluentValidation;

namespace Domain.Validators;

public class SubmittedApplicationValidator : AbstractValidator<SubmittedApplication>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 300;
    private const int MaxPlanLength = 1000;
    
    public SubmittedApplicationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Activity)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength);
        
        RuleFor(x => x.Description)
            .MaximumLength(MaxDescriptionLength);
        
        RuleFor(x => x.Plan)
            .NotEmpty()
            .MaximumLength(MaxPlanLength);

        RuleFor(x => x.CreatedDateTime)
            .NotEmpty();
    }
}