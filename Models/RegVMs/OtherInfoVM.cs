using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class OtherInfoVM
{
    public string PhotoPath { get; set; } = string.Empty;
    public ModeOfReference ModeOfReference { get; set; }
    public string WhyInterested { get; set; } = string.Empty;
    public string Expectations { get; set; } = string.Empty;

}