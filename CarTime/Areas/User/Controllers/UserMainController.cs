using Microsoft.AspNetCore.Mvc;

namespace CarTime.Areas.User.Controllers
{
    [Area("User")]
    public class UserMainController : Controller
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
