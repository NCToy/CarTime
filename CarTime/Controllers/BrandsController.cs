using Microsoft.AspNetCore.Mvc;
using CarTime.Domain;

namespace CarTime.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DataManager dataManager;

        public BrandsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View(dataManager.BrandItems.GetBrandItems());
        }
    }
}