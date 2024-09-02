using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class EducationVM
{
    public InstitutionType InstitutionType { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
    public DegreeType DegreeType { get; set; }
    public Faculty Faculty { get; set; }
    public Department Department { get; set; }
    public CourseOfStudy CourseOfStudy { get; set;}
    public Level Level { get; set; }
    public DateTime YearOfAdmission { get; set; }
    public DateTime YearOfGraduation { get; set;}
}
