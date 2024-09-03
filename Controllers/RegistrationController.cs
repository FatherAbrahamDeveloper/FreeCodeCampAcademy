using FluentValidation;
using FreeCodeCampAcademy.Assets.AppSession;
using FreeCodeCampAcademy.Assets.AppSession.Core;
using FreeCodeCampAcademy.Assets.Enums;
using FreeCodeCampAcademy.Models.RegVMs;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FreeCodeCampAcademy.Controllers;
[Route("register")]

public class RegistrationController(IValidator<BioDataVM> bioValidator, IValidator<ContactVM> contactValidator, IValidator<EducationVM> eduValidator, IValidator<OtherInfoVM> otherInfoValidator, IStartSession sessionStarter, IWebHostEnvironment environment, ICacheUtil cacheUtil) : FreeCodeCampAcademyController(sessionStarter, cacheUtil)

{

    private readonly IValidator<BioDataVM> _bioValidator = bioValidator;
    private readonly IValidator<ContactVM> _contactValidator = contactValidator;
    private readonly IValidator<EducationVM> _eduValidator = eduValidator;
    private readonly IValidator<OtherInfoVM> _otherInfoValidator = otherInfoValidator;
    private readonly IWebHostEnvironment _environment = environment;
    private DirectoryHelpServ _directoryHelpServ = new(environment);
    private readonly StringBuilder _valErrors = new();



    public IActionResult Index(int? progType)
    {
        var calId = progType ?? 1;
        ViewBag.CallId = calId.ToString();
        return View();
    }

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

    [HttpPost("wizard/next")]
    public IActionResult NextStep(int progType, int stage)
    {
        var nextStage = stage < (int)RegStage.GeneralInfo ? stage + 1 : stage;
        return RedirectToAction("RegWizard", new { progType, stage = nextStage });
    }

    [HttpPost("wizard/prev")]
    public IActionResult PrevStep(int progType, int stage)
    {
        var prevStage = stage < (int)RegStage.BioData ? stage - 1 : stage;
        return RedirectToAction("RegWizard", new { progType, stage = prevStage });
    }
    //Biodata
    [HttpPost("add-biodata")]
    public async Task<JsonResult> AddBiodata(BioDataVM model)
    {
        try
        {
            if (model == null)
            {
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });
            }

            var valResult = await _bioValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });
            return Json(new { IsSuccessful = false, IsAuthenticated = true, IsReload = false, Error = "" });

        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

    //Contact
    [HttpPost("add-contact")]
    public async Task<JsonResult> AddContact(ContactVM model)
    {
        try
        {
            if (model == null)
            {
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });
            }

            var valResult = await _contactValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });
            return Json(new { IsSuccessful = false, IsAuthenticated = true, IsReload = false, Error = "" });

        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

    //Education
    [HttpPost("add-education")]
    public async Task<JsonResult> AddEducation(EducationVM model)
    {
        try
        {
            if (model == null)
            {
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });
            }

            var valResult = await _eduValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });
            return Json(new { IsSuccessful = false, IsAuthenticated = true, IsReload = false, Error = "" });

        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

    ////SIWES
    //[HttpPost("add-siwes")]
    //public async Task<JsonResult> AddSiwes(ContactVM model)
    //{
    //    try
    //    {
    //        if (model == null)
    //        {
    //            return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });
    //        }

    //        var valResult = await _siwesValidator.ValidateAsync(model);
    //        if (!valResult.IsValid)
    //            return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });
    //        return Json(new { IsSuccessful = false, IsAuthenticated = true, IsReload = false, Error = "" });

    //    }
    //    catch (Exception ex)
    //    {

    //        UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
    //        return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
    //    }
    //}

    //Education
    [HttpPost("add-otherinfo")]
    public async Task<JsonResult> AddOtherInfo(OtherInfoVM model)
    {
        try
        {
            if (model == null)
            {
                return Json(new { IsSuccessful = false, Error = "Session Expired", IsAuthenticated = true });
            }

            var valResult = await _otherInfoValidator.ValidateAsync(model);
            if (!valResult.IsValid)
                return Json(new { IsSuccessful = false, Error = valResult.Errors.ToErrorListString(), IsAuthenticated = true });
            return Json(new { IsSuccessful = false, IsAuthenticated = true, IsReload = false, Error = "" });

        }
        catch (Exception ex)
        {

            UtilTools.LogE(ex.StackTrace, ex.Source, ex.Message);
            return Json(new { IsAuthenticated = true, IsSuccessful = false, IsReload = false, Error = "Process Error Occurred! Please try again later" });
        }
    }

}

