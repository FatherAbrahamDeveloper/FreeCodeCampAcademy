using FluentValidation;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets.AppKits.RegWizValidations;

public class EducationVal : AbstractValidator<EducationVM>
{
    public EducationVal()
    {
        RuleFor(e => e.InstitutionType).Must(InstitutionType => InstitutionType > 0).WithMessage(UIValidationMsg.INSTITUTION_TYPE_REQUIRED);
        RuleFor(e => e.InstitutionId).Must(InstitutionId => InstitutionId > 0).WithMessage(UIValidationMsg.INSTITUTION_NAME_REQUIRED);
        RuleFor(e => e.FacultyId).Must(FacultyId => FacultyId > 0).WithMessage(UIValidationMsg.FACULTY_REQUIRED);
        RuleFor(e => e.CourseId).Must(CourseId => CourseId > 0).WithMessage(UIValidationMsg.COURSE_OF_STUDY_REQUIRED);
        RuleFor(e => e.Level).Must(Level => Level > 0).WithMessage(UIValidationMsg.LEVEL_REQUIRED);
        RuleFor(e => e.YearOfAdmisssion).Must(YearOfAdmisssion => YearOfAdmisssion > 0).WithMessage(UIValidationMsg.ADMISSION_YEAR_REQUIRED);
        RuleFor(e => e.YearOfGraduation).Must(YearOfGraduation => YearOfGraduation > 0).WithMessage(UIValidationMsg.GRADUATION_YEAR_REQUIRED);
    }
}
