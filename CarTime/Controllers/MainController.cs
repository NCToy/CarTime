 using Microsoft.AspNetCore.Mvc;

namespace CarTime.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
