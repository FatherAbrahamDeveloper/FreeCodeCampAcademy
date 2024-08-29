using Microsoft.AspNetCore.Mvc;

namespace FreeCodeCampAcademy.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
