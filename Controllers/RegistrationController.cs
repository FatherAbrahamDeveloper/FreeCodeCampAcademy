using FluentValidation;
using FreeCodeCampAcademy.Assets.AppSession;
using FreeCodeCampAcademy.Assets.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FreeCodeCampAcademy.Controllers;

[Route("register")]
public class RegistrationController(IValidator<BioDataVM> bioValidator, IValidator<ContactVM> contactValidator, IValidator<EducationVM> eduValidator, IValidator<ProgInfoVM> progValidator, IValidator<OtherInfoVM> otherValidator, IStartSession sessionStarter, IWebHostEnvironment environment, ICacheUtil cacheUtil) : FreeCodeCampAcademyController(sessionStarter, cacheUtil)

{

    private readonly IValidator<BioDataVM> _bioValidator = bioValidator;
    private readonly IValidator<ContactVM> _contactValidator = contactValidator;
    private readonly IValidator<EducationVM> _eduValidator = eduValidator;
    private readonly IValidator<ProgInfoVM> _progValidator = progValidator;
    private readonly IValidator<OtherInfoVM> _otherValidator = otherValidator;
    private readonly IWebHostEnvironment _environment = environment;
    private DirectoryHelpServ _directoryHelpServ = new(environment);
    private readonly StringBuilder _valErrors = new();

    public IActionResult Index(int? progType)
    {
        var calId = progType ?? 1;
        ViewBag.CallId = calId.ToString();
        return View();
    }

    [HttpGet("wizard")]
    public IActionResult RegWizard(int? progType, int? stage)
    {
        ViewBag.Error = "";
        var calId = progType ?? 1;
        var stageId = stage ?? (int)RegStage.BioData;
        var resp = GetRegFromStore();
        if (resp.Failed)
        {
            ViewBag.Error = resp.Errors[0];
            return View(new RegistrationVM());
        }

        var reg = resp.Value;
        reg.RegStage = (RegStage)stageId;
        reg.RegType = (RegType)calId;
        return View(reg);
    }

    #region Upload Passport
    [HttpPost("upload-photo")]
    public JsonResult ProcessPhotoUpload()
    {
        try
        {
            var formFile = Request.Form.Files["photoUpload"];
            if (formFile == null)
            {
                return Json(new { IsSuccessful = false, Error = "Upload Failed! Try again later", IsAuthenticated = true });
            }

            var resp = GetRegFromStore();
            if (resp.Failed || resp.Value == null)
            {
                return Json(new { IsSuccessful = false, Error = "Invalid Session! Please try again later", IsAuthenticated = true });
            }
            var regInfo = resp.Value;
            var filePath = _directoryHelpServ.SaveFile(formFile, regInfo.ApplicationId.ToString().Replace("-", "_"));
            if (string.IsNullOrEmpty(filePath) || filePath.Length < 5)
            {
                var msg = string.IsNullOrEmpty(_directoryHelpServ.MessageInfo) ? "Upload Failed! Please try again later" : _directoryHelpServ.MessageInfo;
                return Json(new { IsSuccessful = false, Error = msg, IsAuthenticated = true });
            }
            regInfo.PhotoPath = filePath;
            SaveRegToStore(regInfo);

            return Json(new { IsAuthenticated = true, IsSuccessful = true, PhotoPath = filePath, IsReload = false, Error = "" });
        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }


    #endregion

    [HttpPost("process-biodata")]
    public async Task<JsonResult> ProcessBiodata(BioDataVM model)
    {
        try
        {

            if (model == null)
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });

            var valResult = await _bioValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });

            var resp = GetRegFromStore();
            if (resp.Failed)
                return Json(new { IsSuccessful = false, Error = resp.Errors[0], IsAuthenticated = true });

            var reg = resp.Value;

            if (reg == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Application Session!", IsAuthenticated = true });

            if (IsNullOrEmpty(reg.PhotoPath) || reg.PhotoPath.Length < 6)
                return Json(new { IsSuccessful = false, Error = "Kindly attach your photograph", IsAuthenticated = true });

            reg.BioData ??= new BioDataVM();
            reg.BioData.PhotoPath = reg.PhotoPath;
            reg.BioData.Surname = model.Surname;
            reg.BioData.MaritalStatus = model.MaritalStatus;
            reg.BioData.DateOfBirth = model.DateOfBirth;
            reg.BioData.FirstName = model.FirstName;
            reg.BioData.Email = model.Email;
            reg.BioData.Gender = model.Gender;
            reg.BioData.MobileNo = model.MobileNo;
            reg.BioData.OtherNames = model.OtherNames;

            reg.Contact ??= new ContactVM();
            reg.Contact.PhotoPath = reg.PhotoPath;

            var result = SaveRegToStore(reg);
            if (resp.Failed)
                return Json(new { IsSuccessful = false, Error = result.Errors[0], IsAuthenticated = true });

            return Json(new { IsAuthenticated = true, IsSuccessful = true, IsReload = false, Error = "" });
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }
    //Contact
    [HttpPost("process-contact")]
    public async Task<JsonResult> ProcessContact(ContactVM model)
    {
        try
        {

            if (model == null)
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });

            var valResult = await _contactValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });

            var resp = GetRegFromStore();
            if (resp.Failed)
                return Json(new { IsSuccessful = false, Error = resp.Errors[0], IsAuthenticated = true });

            var reg = resp.Value;

            if (reg == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Application Session!", IsAuthenticated = true });

            if (IsNullOrEmpty(reg.PhotoPath) || reg.PhotoPath.Length < 6)
                return Json(new { IsSuccessful = false, Error = "Kindly attach your photograph", IsAuthenticated = true });

            var valResult2 = await _bioValidator.ValidateAsync(reg.BioData);
            if (!valResult2.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult2.Errors.ToErrorListString(), IsAuthenticated = true });


            reg.Contact ??= new ContactVM();
            reg.Contact.PhotoPath = reg.PhotoPath;
            reg.Contact.LandMark = model.LandMark;
            reg.Contact.Area = model.Area;
            reg.Contact.City = model.City;
            reg.Contact.StateId = model.StateId;
            reg.Contact.LocalAreaId = model.LocalAreaId;
            reg.Contact.HouseNo = model.HouseNo;
            reg.Contact.Street = model.Street;

            reg.Education ??= new EducationVM();
            reg.Education.PhotoPath = reg.PhotoPath;


            return Json(new { IsAuthenticated = true, IsSuccessful = true, IsReload = false, Error = "" });
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }
    //Education
    [HttpPost("process-education")]
    public async Task<JsonResult> ProcessEducation(EducationVM model)
    {
        try
        {

            if (model == null)
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });

            var valResult = await _eduValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });

            if (model.YearOfAdmisssion > DateTime.Now.Year)
                return Json(new { IsSuccessful = false, Error = "Invalid Year of Admission!", IsAuthenticated = true });

            if (model.YearOfAdmisssion >= model.YearOfGraduation)
                return Json(new { IsSuccessful = false, Error = "Invalid Year of Admission / Graduation!", IsAuthenticated = true });

            if (model.YearOfGraduation - model.YearOfAdmisssion < 2)
                return Json(new { IsSuccessful = false, Error = "Invalid Year of Admission / Graduation!", IsAuthenticated = true });

            var resp = GetRegFromStore();
            if (resp.Failed)
                return Json(new { IsSuccessful = false, Error = resp.Errors[0], IsAuthenticated = true });

            var reg = resp.Value;

            if (reg == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Application Session!", IsAuthenticated = true });

            if (IsNullOrEmpty(reg.PhotoPath) || reg.PhotoPath.Length < 6)
                return Json(new { IsSuccessful = false, Error = "Kindly attach your photograph", IsAuthenticated = true });

            if (reg.BioData == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Bio data Information", IsAuthenticated = true });

            var valResult2 = await _bioValidator.ValidateAsync(reg.BioData);
            if (!valResult2.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult2.Errors.ToErrorListString(), IsAuthenticated = true });

            if (reg.Contact == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Contact Information", IsAuthenticated = true });

            var valResult3 = await _contactValidator.ValidateAsync(reg.Contact);
            if (!valResult3.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult3.Errors.ToErrorListString(), IsAuthenticated = true });

            reg.Education ??= new EducationVM();
            reg.Education.PhotoPath = reg.PhotoPath;
            reg.Education.FacultyId = model.FacultyId;
            reg.Education.CourseId = model.CourseId;
            reg.Education.YearOfAdmisssion = model.YearOfAdmisssion;
            reg.Education.YearOfGraduation = model.YearOfGraduation;
            reg.Education.InstitutionId = model.InstitutionId;
            reg.Education.InstitutionType = model.InstitutionType;
            reg.Education.Level = model.Level;

            reg.ProgInfo ??= new ProgInfoVM();
            reg.ProgInfo.PhotoPath = reg.PhotoPath;

            return Json(new { IsAuthenticated = true, IsSuccessful = true, IsReload = false, Error = "" });
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

    //Program Info
    [HttpPost("process-prog-info")]
    public async Task<JsonResult> ProcessProgInfo(ProgInfoVM model)
    {
        try
        {

            if (model == null)
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });

            var valResult = await _progValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });

            var resp = GetRegFromStore();
            if (resp.Failed)
                return Json(new { IsSuccessful = false, Error = resp.Errors[0], IsAuthenticated = true });

            var reg = resp.Value;

            if (reg == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Application Session!", IsAuthenticated = true });

            if (IsNullOrEmpty(reg.PhotoPath) || reg.PhotoPath.Length < 6)
                return Json(new { IsSuccessful = false, Error = "Kindly attach your photograph", IsAuthenticated = true });

            if (reg.BioData == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Bio data Information", IsAuthenticated = true });

            var valResult2 = await _bioValidator.ValidateAsync(reg.BioData);
            if (!valResult2.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult2.Errors.ToErrorListString(), IsAuthenticated = true });

            if (reg.Contact == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Contact Information", IsAuthenticated = true });

            var valResult3 = await _contactValidator.ValidateAsync(reg.Contact);
            if (!valResult3.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult3.Errors.ToErrorListString(), IsAuthenticated = true });

            if (reg.Education == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Education Information", IsAuthenticated = true });

            var valResult4 = await _eduValidator.ValidateAsync(reg.Education);
            if (!valResult4.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult4.Errors.ToErrorListString(), IsAuthenticated = true });

            reg.ProgInfo ??= new ProgInfoVM();
            reg.ProgInfo.PhotoPath = reg.PhotoPath;
            reg.ProgInfo.ProgDuration = model.ProgDuration;
            reg.ProgInfo.ProgType = model.ProgType;
            reg.ProgInfo.CareerInterest = model.CareerInterest;
            reg.ProgInfo.CareerObjective = model.CareerObjective;
            reg.ProgInfo.StartDate = model.StartDate;
            reg.ProgInfo.EndDate = model.EndDate;

            reg.OtherInfo ??= new OtherInfoVM();
            reg.OtherInfo.PhotoPath = reg.PhotoPath;


            return Json(new { IsAuthenticated = true, IsSuccessful = true, IsReload = false, Error = "" });
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

    //Other Info
    [HttpPost("process-gen-info")]
    public async Task<JsonResult> ProcessOthersInfo(OtherInfoVM model)
    {
        try
        {

            if (model == null)
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });

            var valResult = await _otherValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });

            var resp = GetRegFromStore();
            if (resp.Failed)
                return Json(new { IsSuccessful = false, Error = resp.Errors[0], IsAuthenticated = true });

            var reg = resp.Value;

            if (reg == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Application Session!", IsAuthenticated = true });

            if (IsNullOrEmpty(reg.PhotoPath) || reg.PhotoPath.Length < 6)
                return Json(new { IsSuccessful = false, Error = "Kindly attach your photograph", IsAuthenticated = true });

            if (reg.BioData == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Bio data Information", IsAuthenticated = true });

            var valResult2 = await _bioValidator.ValidateAsync(reg.BioData);
            if (!valResult2.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult2.Errors.ToErrorListString(), IsAuthenticated = true });

            if (reg.Contact == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Contact Information", IsAuthenticated = true });

            var valResult3 = await _contactValidator.ValidateAsync(reg.Contact);
            if (!valResult3.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult3.Errors.ToErrorListString(), IsAuthenticated = true });

            if (reg.Education == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Education Information", IsAuthenticated = true });

            var valResult4 = await _eduValidator.ValidateAsync(reg.Education);
            if (!valResult4.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult4.Errors.ToErrorListString(), IsAuthenticated = true });

            if (reg.ProgInfo == null)
                return Json(new { IsSuccessful = false, Error = "Invalid Program Information", IsAuthenticated = true });

            var valResult5 = await _progValidator.ValidateAsync(reg.ProgInfo);
            if (!valResult5.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult5.Errors.ToErrorListString(), IsAuthenticated = true });

            reg.OtherInfo ??= new OtherInfoVM();
            reg.OtherInfo.PhotoPath = reg.PhotoPath;
            reg.OtherInfo.WhyInterested = model.WhyInterested;
            reg.OtherInfo.ModeOfReference = model.ModeOfReference;
            reg.OtherInfo.Expectations = model.Expectations;

            //reg.OtherInfo ??= new OtherInfoVM();
            //reg.OtherInfo.PhotoPath = reg.PhotoPath;


            return Json(new { IsAuthenticated = true, IsSuccessful = true, IsReload = false, Error = "" });
        }
        catch (Exception ex)
        {
            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

  

}

