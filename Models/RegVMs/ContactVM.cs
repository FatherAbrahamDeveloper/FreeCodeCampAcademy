using FreeCodeCampAcademy.Models.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class ContactVM
{
    public string PhotoPath { get; set; } = Empty;
    public string HouseNo { get; set; } = Empty;
    public string Street { get; set; } = Empty;
    public string Area { get; set; } = Empty;
    public string City { get; set; } = Empty;
    public string LandMark { get; set; } = Empty;
    public int LocalAreaId { get; set; }
    public int StateId { get; set; }
}

}
