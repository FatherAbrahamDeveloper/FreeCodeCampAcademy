using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class BioDataVal : AbstractValidator<BioDataVM>
{

    public BioDataVal()
    {
        RuleFor(e => e.Surname).NotEmpty().WithMessage(UIValidationMsg.SURNAME_REQUIRED).Length(3, 50);
        RuleFor(e => e.FirstName).NotEmpty().WithMessage(UIValidationMsg.FIRST_NAME_REQUIRED).Length(3, 50);
        RuleFor(e => e.Email).NotEmpty().WithMessage(UIValidationMsg.EMAIL_REQUIRED).EmailAddress().WithMessage("A valid email is required");
        RuleFor(e => e.MobileNo).NotEmpty().Length(11, 11)
        .WithMessage(UIValidationMsg.MOBILE_NUMBER_REQUIRED)
        .Must((biodata, mobileNo) => biodata.MobileNo.IsMobileNumberValid())
        .WithMessage(UIValidationMsg.INVALID_MOBILE_NUMBER);
        RuleFor(e => e.DateOfBirth).NotEmpty().WithMessage(UIValidationMsg.DATE_OF_BIRTH_REQUIRED);
        RuleFor(e => e.Gender).NotEmpty().WithMessage(UIValidationMsg.GENDER_REQUIRED);
        RuleFor(e => e.MaritalStatus).NotEmpty().WithMessage(UIValidationMsg.MARITAL_STATUS_REQUIRED);

    }
}
