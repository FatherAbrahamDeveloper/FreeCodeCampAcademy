using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class EducationVal : AbstractValidator<EducationVM>
{
    public EducationVal()
    {
        RuleFor(e => e.InstitutionType).NotEmpty().WithMessage(UIValidationMsg.INSTITUTION_TYPE_REQUIRED);
        RuleFor(e => e.InstitutionName).NotEmpty().WithMessage(UIValidationMsg.INSTITUTION_NAME_REQUIRED);
        RuleFor(e => e.DegreeType).NotEmpty().WithMessage(UIValidationMsg.DEGREE_TYPE_REQUIRED);
        RuleFor(e => e.Faculty).NotEmpty().WithMessage(UIValidationMsg.FACULTY_REQUIRED);
        RuleFor(e => e.Department).NotEmpty().WithMessage(UIValidationMsg.DEPARTMENT_REQUIRED);
        RuleFor(e => e.CourseOfStudy).NotEmpty().WithMessage(UIValidationMsg.COURSE_OF_STUDY_REQUIRED);
        RuleFor(e => e.Level).NotEmpty().WithMessage(UIValidationMsg.LEVEL_REQUIRED);
        RuleFor(e => e.YearOfAdmission).NotEmpty().WithMessage(UIValidationMsg.GENERALLY_REQUIRED);
        RuleFor(e => e.YearOfGraduation).NotEmpty().WithMessage(UIValidationMsg.GENERALLY_REQUIRED);
    }
}
