using FluentValidation;
using FreeCodeCampAcademy.Models.Enums;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class ProgInfoVal : AbstractValidator<ProgInfoVM>
{
    public ProgInfoVal()
    {
        RuleFor(e => e.CareerInterest).Must(CareerInterest => CareerInterest > 0).WithMessage(UIValidationMsg.CAREER_INTEREST_REQUIRED);
        RuleFor(e => e.ProgDuration).Must(ProgDuration => ProgDuration > 0).WithMessage(UIValidationMsg.PROGRAM_DURATION_REQUIRED);
        RuleFor(e => e.StartDate).NotEmpty().WithMessage(UIValidationMsg.PROGRAM_START_DATE_REQUIRED).Length(10, 10);
        RuleFor(e => e.EndDate).NotEmpty().WithMessage(UIValidationMsg.PROGRAM_END_DATE_REQUIRED).Length(10, 10);
        RuleFor(e => e.CareerObjective).NotEmpty().WithMessage(UIValidationMsg.CAREER_OBJECTIVE_REQUIRED).Length(10, 300);
    }
}
