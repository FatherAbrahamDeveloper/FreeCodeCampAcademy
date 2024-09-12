using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class EducationVM
{
    public string PhotoPath { get; set; } = string.Empty;
    public InstitutionType InstitutionType { get; set; }
    public int InstitutionId { get; set; }
    public int FacultyId { get; set; }
    public int CourseId { get; set; }
    public Level Level { get; set; }
    public int YearOfAdmisssion { get; set; }
    public int YearOfGraduation { get; set; }
}
