using FreeCodeCampAcademy.Assets.Enums;
//using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class ProgInfoVM
{
    public ProgDuration ProgDuration { get; set; }
    public RegType RegType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CareerInterest CareerInterest { get; set; }
    public string CareerObjective { get; set; }
}
