using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarTime.Domain;
using CarTime.Models;
using CarTime.Service;

namespace CarTime.Controllers
{
    public class ReturnCarController : Controller
    {
        private readonly AppDbContext _context;

        public ReturnCarController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ReturnOrder returnOrder = new ReturnOrder();
            return View(returnOrder);
        }
        [HttpPost]
        public IActionResult Index(ReturnOrder returnOrder)
        {
            if (ModelState.IsValid)
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == returnOrder.OrderId);
                if (order != null)
                {
                    returnOrder.DateAdding = DateTime.UtcNow;
                    returnOrder.Status = "created";
                    _context.ReturnOrders.Add(returnOrder);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(ReturnCarController.Thanks), nameof(ReturnCarController).CutController());
                }
            }
            return View(returnOrder);
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
