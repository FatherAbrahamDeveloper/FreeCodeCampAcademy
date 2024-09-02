using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class BioDataVM
{
    public string Surname { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string OtherNames { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string MobileNo { get; set; } = default!;
    public string Email { get; set; } = default!;
    public MaritalStatus MaritalStatus { get; set; }
}
