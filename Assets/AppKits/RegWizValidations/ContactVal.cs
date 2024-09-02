using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class ContactVal : AbstractValidator<ContactVM>
{
    public ContactVal()
    {
        RuleFor(e => e.ResidentialAddress).NotEmpty().MinimumLength(20).WithMessage(UIValidationMsg.RESIDENTIAL_ADDRESS_REQUIRED);
        RuleFor(e => e.City).NotEmpty().MinimumLength(3).WithMessage(UIValidationMsg.CITY_REQUIRED);
        RuleFor(e => e.LGAResidence).NotEmpty().MinimumLength(3).WithMessage(UIValidationMsg.LGA_REQUIRED);
        RuleFor(e => e.StateOfOrigin).NotEmpty().WithMessage(UIValidationMsg.STATE_OF_ORIGIN_REQUIRED);
    }
}