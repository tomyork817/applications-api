using Domain.Applications;
using FluentValidation;

namespace Domain.Validators;

public class ApplicationActivityValidator : AbstractValidator<ApplicationActivity>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 300;
    
    public ApplicationActivityValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(MaxDescriptionLength);
    }
}