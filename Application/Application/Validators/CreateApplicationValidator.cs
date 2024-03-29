using Application.Contracts.Applications;
using FluentValidation;

namespace Application.Validators;

public class CreateApplicationValidator : AbstractValidator<CreateApplication.Command>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 300;
    private const int MaxPlanLength = 1000;
    
    public CreateApplicationValidator()
    {
        RuleFor(x => x.Author)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .MaximumLength(MaxNameLength);
        
        RuleFor(x => x.Description)
            .MaximumLength(MaxDescriptionLength);
        
        RuleFor(x => x.Outline)
            .MaximumLength(MaxPlanLength);

        RuleFor(x => x)
            .Must(x => x.Name is not null || x.Description is not null || x.Outline is not null || x.Activity is not null)
            .WithMessage("Not all required fields are filled in application to submit");
    }
}