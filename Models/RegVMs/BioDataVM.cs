using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class BioDataVM
{
    public string PhotoPath { get; set; } = Empty;
    public string Surname { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string OtherNames { get; set; } = default!;
    public string DateOfBirth { get; set; } = DateTime.Today.AddYears(-16).ToString("yyyy-MM-dd");
    public string MaxDateOfBirth { get; set; } = DateTime.Now.AddYears(-16).ToString("yyyy-MM-dd");
    public Gender Gender { get; set; }
    public string MobileNo { get; set; } = default!;
    public string Email { get; set; } = default!;
    public MaritalStatus MaritalStatus { get; set; }
}
