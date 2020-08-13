using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Service;

namespace CarTime.Areas.Admin.Controllers
{
    [Area("Manager")]
    public class EditBrandController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostingEnv;

        public EditBrandController(DataManager dataManager, IWebHostEnvironment hostingEnv)
        {
            this._dataManager = dataManager;
            this._hostingEnv = hostingEnv;
        }

        public IActionResult Index()
        {
            IQueryable<BrandItem> entities = _dataManager.BrandItems.GetBrandItems();
            return View(entities);
        }

        public IActionResult Add()
        {
            ViewBag.Except = false;
            BrandItem brand = new BrandItem();
            return View(brand);
        }
        [HttpPost]
        public IActionResult Add(BrandItem modell, IFormFile filePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (filePath != null)
                    {
                        modell.TitleImagePath = filePath.FileName;
                        using (var stream =
                            new FileStream(Path.Combine(_hostingEnv.WebRootPath, "img/brands/", filePath.FileName),
                                FileMode.Create))
                        {
                            filePath.CopyTo(stream);
                        }
                    }

                    _dataManager.BrandItems.SaveBrandItem(modell);
                    return RedirectToAction(nameof(EditMainController.Index),
                        nameof(EditMainController).CutController());
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Except = true;
                    return View(modell);
                }
                
            }
            return View(modell);
        }

        public IActionResult Edit(Guid id)
        {
            return View(_dataManager.BrandItems.GetBrandItemById(id));
        }
        [HttpPost]
        public IActionResult Edit(BrandItem modell, IFormFile filePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (filePath != null)
                    {
                        modell.TitleImagePath = filePath.FileName;
                        using (var stream =
                            new FileStream(Path.Combine(_hostingEnv.WebRootPath, "img/brands/", filePath.FileName),
                                FileMode.Create))
                        {
                            filePath.CopyTo(stream);
                        }
                    }

                    _dataManager.BrandItems.SaveBrandItem(modell);
                    return RedirectToAction(nameof(EditBrandController.Index),
                        nameof(EditBrandController).CutController());
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Except = true;
                    return View(modell);
                }
            }

            return View(modell);
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            _dataManager.BrandItems.DeleteBrandItem(Id);
            return RedirectToAction(nameof(EditBrandController.Index), nameof(EditBrandController).CutController());
        }
    }
}
