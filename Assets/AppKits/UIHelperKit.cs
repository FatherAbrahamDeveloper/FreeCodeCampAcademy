using FreeCodeCampAcademy.Assets.Enums;
using FreeCodeCampAcademy.Models.RegVMs;

namespace FreeCodeCampAcademy.Assets;

public class UIHelperKit
{
	public static (string title, string csClass) WizHeading(RegType regType)
	{
		return regType switch
		{
			RegType.SIWES => ("SIWES Registration", "innerheaderbg"),
			RegType.NYSC => ("NYSC Registration", "innerheaderbg"),
			RegType.Starter => ("IT Starter Registration", "innerheaderbg"),
			RegType.Intern => ("Intern Registration", "innerheaderbg"),
			_ => ("SIWES Registration", "innerheaderbg")

		};
	
	}
	public static string WizFormId(RegStage stage)
	{
		switch (stage)
		{
			case RegStage.BioData:
				return "#frmBioData";
            case RegStage.Contact:
                return "#frmContact";
            case RegStage.Education:
                return "#frmEducation";
            case RegStage.ProgramInfo:
                return "#frmSiwes";
            case RegStage.GeneralInfo:
                return "#frmOtherInfo";
            default:
				return "frmSiwes";

		}
	}

	public static (string moduleName, object model) WizModule (RegStage stage, RegistrationVM regModel)
	{
		switch (stage)
		{
			case RegStage.BioData:
				return ("_BioData", regModel.BioData);
			case RegStage.Contact:
                return ("_Contact", regModel.Contact);
            case RegStage.Education:
                return ("_Education", regModel.Education);
            case RegStage.ProgramInfo:
                return ("_ProgInfo", regModel.ProgInfo);
            case RegStage.GeneralInfo:
                return ("_OtherInfo", regModel.OtherInfo);
            default:
				return ("_BioData", regModel.BioData);	
		}

	}
   
}
