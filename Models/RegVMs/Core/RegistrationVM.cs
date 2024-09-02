using FreeCodeCampAcademy.Assets.Enums;

namespace FreeCodeCampAcademy.Models.RegVMs;

public class RegistrationVM
{
    public Guid ApplicationId { get; set; } = default!;
    public string PhotoPath { get; set; } = default!;
    public bool IsSubmitted { get; set; }
    public BioDataVM BioData { get; set; } = default!;
    public ContactVM Contact { get; set; } = default!;
    public EducationVM Education { get; set; } = default!;
    public ProgInfoVM ProgInfo { get; set; } = default!;
    public OtherInfoVM OtherInfo { get; set; } = default!;

}
