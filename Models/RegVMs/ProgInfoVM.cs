using FreeCodeCampAcademy.Assets.Enums;
using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class ProgInfoVM

{
    public string PhotoPath { get; set; }   = string.Empty;
    public ProgDuration ProgDuration { get; set; }
    public RegType RegType { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public CareerInterest CareerInterest { get; set; }
    public string CareerObjective { get; set; } = string.Empty;
}
