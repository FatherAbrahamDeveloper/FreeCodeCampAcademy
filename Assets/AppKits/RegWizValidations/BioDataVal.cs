using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class BioDataVal : AbstractValidator<BioDataVM>
{

    public BioDataVal()
    {
        RuleFor(e => e.Surname).NotEmpty().MinimumLength(3).WithMessage(UIValidationMsg.SURNAME_REQUIRED);
        RuleFor(e => e.FirstName).NotEmpty().MinimumLength(3).WithMessage(UIValidationMsg.FIRST_NAME_REQUIRED);
        RuleFor(e => e.OtherNames).NotEmpty().MinimumLength(3).WithMessage(UIValidationMsg.OTHER_NAMES_REQUIRED);
        RuleFor(e => e.Email).NotEmpty().MinimumLength(5).WithMessage(UIValidationMsg.EMAIL_REQUIRED);
        RuleFor(e => e.MobileNo).NotEmpty().MinimumLength(11).WithMessage(UIValidationMsg.PHONE_NUMBER_REQUIRED);
        RuleFor(e => e.DateOfBirth).NotEmpty().WithMessage(UIValidationMsg.DATE_OF_BIRTH_REQUIRED);
        RuleFor(e => e.Gender).NotEmpty().WithMessage(UIValidationMsg.GENDER_REQUIRED);
        RuleFor(e => e.Surname).NotEmpty().WithMessage(UIValidationMsg.MARITAL_STATUS_REQUIRED);

    }
}
