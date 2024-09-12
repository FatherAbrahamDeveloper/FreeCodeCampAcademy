using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.Controllers.Datas;

public class LookupDataController: Controller
{
    public IActionResult Index()

    {
        return View();
    }
    #region Cascade

    [HttpGet("load-lgas")]
    public IActionResult _LoadLocalAreaComponent(int stateId, int selectedId)
    {

        return ViewComponent("LocalAreaDDL", new { stateId, selectedId });  
    }

    [HttpGet("load-institutions")]
    public IActionResult _LoadInstitutionsAreaComponent(int instType, int selectedId)
    {

        return ViewComponent("InstitutionDDL", new { instType, selectedId });
    }
    [HttpGet("load-courses")]
    public IActionResult _LoadCourseComponent(int facultyId, int selectedId)
    {

        return ViewComponent("CourseDDL", new { facultyId, selectedId });
    }

    #endregion
}
