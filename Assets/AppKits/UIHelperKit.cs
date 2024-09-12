using FreeCodeCampAcademy.Assets.Enums;

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

	public static ModuleIDVM WizIdHelp(RegistrationVM regModel)
	{
		var item = new ModuleIDVM();
		if (regModel.Education !=null)
		{
			item.FacultyId = regModel.Education.FacultyId;
			item.InstType = (int)regModel.Education.InstitutionType;
			item.SelInstId = regModel.Education.InstitutionId;
			item.SelCourseId = regModel.Education.CourseId;
		}

        if (regModel.Contact != null)
        {
            item.StateId = regModel.Contact.StateId;
            item.SelLocalAreaId = regModel.Contact.LocalAreaId;
            
        }
        return item;
	}

	public static ModuleHelpVM WizModuleHelp(RegistrationVM regModel)
	{
		var stage = regModel.RegStage;

		switch (stage)
		{
			case RegStage.BioData:
				return new ModuleHelpVM
				{ CurrentServiceUrl = "/register/process-biodata",
					CurrentStage = stage,
					FormId = "#frmBiodata",
					NextServiceUrl = "/register/process-contact",
					NextStage = RegStage.Contact,
					PrevServiceUrl = "",
					PrevStage = stage,
					RegType = regModel.RegType				
				
				};
								
			case RegStage.Contact:
                return new ModuleHelpVM
                {
                    CurrentServiceUrl = "/register/process-contact",
                    CurrentStage = stage,
                    FormId = "#frmContact",
                    NextServiceUrl = "/register/process-education",
                    NextStage = RegStage.Education,
                    PrevServiceUrl = "/register/process-biodata",
                    PrevStage = RegStage.BioData,
                    RegType = regModel.RegType

                };
            case RegStage.Education:
                return new ModuleHelpVM
                {
                    CurrentServiceUrl = "/register/process-education",
                    CurrentStage = stage,
                    FormId = "#frmEducation",
                    NextServiceUrl = "/register/process-prog-info",
                    NextStage = RegStage.ProgramInfo,
                    PrevServiceUrl = "/register/process-contact",
                    PrevStage = RegStage.Contact,
                    RegType = regModel.RegType

                };
            case RegStage.ProgramInfo:
                return new ModuleHelpVM
                {
                    CurrentServiceUrl = "/register/process-prog-info",
                    CurrentStage = stage,
                    FormId = "#frmProgInfo",
                    NextServiceUrl = "/register/process-gen-info",
                    NextStage = RegStage.GeneralInfo,
                    PrevServiceUrl = "/register/process-biodata",
                    PrevStage = RegStage.Education,
                    RegType = regModel.RegType

                };
            case RegStage.GeneralInfo:
                return new ModuleHelpVM
                {
                    CurrentServiceUrl = "/register/process-gen-info",
                    CurrentStage = stage,
                    FormId = "#frmGenInfo",
                    NextServiceUrl = "",
                    NextStage = RegStage.Submission,
                    PrevServiceUrl = "/register/process-prog-info",
                    PrevStage = RegStage.ProgramInfo,
                    RegType = regModel.RegType

                };
            default:
                return new ModuleHelpVM
                {
                    CurrentServiceUrl = "/register/process-biodata",
                    CurrentStage = stage,
                    FormId = "#frmBiodata",
                    NextServiceUrl = "/register/process-contact",
                    NextStage = RegStage.Contact,
                    PrevServiceUrl = "",
                    PrevStage = stage,
                    RegType = regModel.RegType

                };
        }

		
	}


}
