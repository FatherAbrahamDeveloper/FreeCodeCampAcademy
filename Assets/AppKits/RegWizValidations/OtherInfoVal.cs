using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class OtherInfoVal : AbstractValidator<OtherInfoVM>
{
    public OtherInfoVal()
    {
        RuleFor(e => e.ModeOfReference).NotEmpty().WithMessage(UIValidationMsg.GENERALLY_REQUIRED);
        RuleFor(e => e.WhyInterested).NotEmpty().WithMessage(UIValidationMsg.GENERALLY_REQUIRED);
    }
}
