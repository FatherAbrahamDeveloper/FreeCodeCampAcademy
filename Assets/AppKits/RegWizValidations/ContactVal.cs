using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class ContactVal : AbstractValidator<ContactVM>
{
    public ContactVal()
    {

        RuleFor(e => e.HouseNo).NotEmpty().WithMessage(UIValidationMsg.HOUSE_NO_REQUIRED)
            .Length(1, 20).WithMessage(UIValidationMsg.HOUSE_NO_LENGTH);
        RuleFor(e => e.Street).NotEmpty().WithMessage(UIValidationMsg.STREET_NAME_REQUIRED)
            .Length(3, 150).WithMessage(UIValidationMsg.STREET_NAME_LENGTH);
        RuleFor(e => e.Area).NotEmpty().WithMessage(UIValidationMsg.AREA_REQUIRED)
            .Length(2, 100).WithMessage(UIValidationMsg.AREA_LENGTH);
        RuleFor(e => e.City).NotEmpty().WithMessage(UIValidationMsg.CITY_REQUIRED)
            .Length(2, 100).WithMessage(UIValidationMsg.CITY_LENGTH);
        RuleFor(e => e.LandMark).NotEmpty().WithMessage(UIValidationMsg.LANDMARK_REQUIRED)
           .Length(2, 100).WithMessage(UIValidationMsg.LANDMARK_LENGTH);
        RuleFor(e => e.LocalAreaId).ExclusiveBetween(1, 774).WithMessage(UIValidationMsg.LGA_REQUIRED);
        RuleFor(e => e.StateId).ExclusiveBetween(1, 37).WithMessage(UIValidationMsg.STATE_REQUIRED);
    }
}