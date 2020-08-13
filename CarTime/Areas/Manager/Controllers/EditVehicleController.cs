using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Domain.Repositories.EntityFramework;
using CarTime.Service;

namespace CarTime.Areas.Admin.Controllers
{
    [Area("Manager")]
    public class EditVehicleController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostingEnv;

        public EditVehicleController(DataManager dataManager, IWebHostEnvironment hostingEnv, AppDbContext context)
        {
            _context = context;
            _dataManager = dataManager;
            _hostingEnv = hostingEnv;
        }

        public IActionResult IndexCar()
        {
            List<CarItem> cars = _context.CarItems.ToList();
            return View(cars);
        }

        public IActionResult AddCar()
        {
            CarItem vehicle = new CarItem();
            ViewBag.Brands = _dataManager.BrandItems.GetBrandItems();
            ViewBag.Except = false;
            return View(vehicle);
        }
        [HttpPost]
        public IActionResult AddCar(CarItem modell, IFormFile filePath, bool newCar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (filePath != null)
                    {
                        modell.TitleImagePath = filePath.FileName;
                        using (var stream =
                            new FileStream(Path.Combine(_hostingEnv.WebRootPath, "img/cars/", filePath.FileName),
                                FileMode.Create))
                        {
                            filePath.CopyTo(stream);
                        }
                    }

                    _dataManager.CarItems.SaveCar(modell);
                    if (newCar == true)
                        return RedirectToAction("AddCar", "EditVehicle");
                    else
                        return RedirectToAction("Index", "EditMain");
                }
                catch (Exception)
                {
                    ViewBag.Except = true;
                    ViewBag.Brands = _dataManager.BrandItems.GetBrandItems();
                    return View(modell);
                }

            }
            ViewBag.Brands = _dataManager.BrandItems.GetBrandItems();
            return View(modell);
        }

        public IActionResult EditCar(Guid id)
        {
            ViewBag.Brands = _dataManager.BrandItems.GetBrandItems();
            ViewBag.Except = false;
            return View(_dataManager.CarItems.GetCarById(id));
        }
        [HttpPost]
        public IActionResult EditCar(CarItem modell, IFormFile filePath)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (filePath != null)
                    {
                        modell.TitleImagePath = filePath.FileName;
                        using (var stream =
                            new FileStream(Path.Combine(_hostingEnv.WebRootPath, "img/cars/", filePath.FileName),
                                FileMode.Create))
                        {
                            filePath.CopyTo(stream);
                        }
                    }

                    _dataManager.CarItems.SaveCar(modell);
                    return RedirectToAction(nameof(EditVehicleController.IndexCar),
                        nameof(EditVehicleController).CutController());
                }
                catch (Exception)
                {
                    ViewBag.Brands = _dataManager.BrandItems.GetBrandItems();
                    ViewBag.Except = true;
                    return View(modell);
                }
            }
            ViewBag.Brands = _dataManager.BrandItems.GetBrandItems();
            return View(modell);
        }

        public IActionResult Delete(CarItem modell)
        {
            _dataManager.CarItems.DeleteCar(modell.Id);
            return RedirectToAction(nameof(EditMainController.Index), nameof(EditMainController).CutController());
        }
    }
}
