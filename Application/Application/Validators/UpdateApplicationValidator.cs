using Application.Contracts.Applications;
using Domain.Constants;
using FluentValidation;

namespace Application.Validators;

public class UpdateApplicationValidator : AbstractValidator<UpdateApplication.Command>
{
    public UpdateApplicationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .MaximumLength(ApplicationConstants.MaxNameLength);
        
        RuleFor(x => x.Description)
            .MaximumLength(ApplicationConstants.MaxDescriptionLength);
        
        RuleFor(x => x.Outline)
            .MaximumLength(ApplicationConstants.MaxOutlineLength);

        RuleFor(x => x)
            .Must(x => x.Name is not null || x.Description is not null || x.Outline is not null || x.Activity is not null)
            .WithMessage("Not all required fields are filled in application to submit");
    }   
}