using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class ProgInfoVal : AbstractValidator<ProgInfoVM>
{
    public ProgInfoVal()
    {
        //RuleFor(e => e.SiwesDuration).NotEmpty().WithMessage(UIValidationMsg.SIWES_DURATION_REQUIRED);
        //RuleFor(e => e.SiwesStartDate).NotEmpty().WithMessage(UIValidationMsg.GENERALLY_REQUIRED);
        //RuleFor(e => e.SiwesEndDate).NotEmpty().WithMessage(UIValidationMsg.GENERALLY_REQUIRED);
        //RuleFor(e => e.AreaOfInterest).NotEmpty().MinimumLength(5).WithMessage(UIValidationMsg.AREA_OF_INTEREST_REQUIRED);
        //RuleFor(e => e.SiwesLetterUpload).NotEmpty().WithMessage(UIValidationMsg.SIWES_UPLOAD_REQUIRED);
        //RuleFor(e => e.CVUpload).NotEmpty().WithMessage(UIValidationMsg.CV_UPLOAD_REQUIRED);
    }
}
