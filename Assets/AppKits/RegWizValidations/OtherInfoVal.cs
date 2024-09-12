using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class OtherInfoVal : AbstractValidator<OtherInfoVM>
{
    public OtherInfoVal()
    {
        RuleFor(e => e.ModeOfReference).Must(ModeOfReference => ModeOfReference > 0)
           .WithMessage(UIValidationMsg.REFER_MODE_REQUIRED);
        RuleFor(e => e.WhyInterested).NotEmpty().Length(10, 300).WithMessage(UIValidationMsg.REASON_REQUIRED);
        RuleFor(e => e.Expectations).NotEmpty().Length(10, 300).WithMessage(UIValidationMsg.EXPECTATION_REQUIRED);
    }
}
