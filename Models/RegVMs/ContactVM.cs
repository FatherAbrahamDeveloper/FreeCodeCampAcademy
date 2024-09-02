using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class ContactVM
{
    public string ResidentialAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string LGAResidence { get; set; } = string.Empty;
    public StateOfOrigin StateOfOrigin { get; set; }
}
