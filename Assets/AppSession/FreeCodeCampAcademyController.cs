using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.Assets.AppSession
{
    public class FreeCodeCampAcademyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
