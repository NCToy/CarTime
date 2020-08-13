using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarTime.Areas.Admin.Controllers
{
    [Area("Manager")]
    public class EditMainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
