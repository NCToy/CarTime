using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Models;
using CarTime.Service;

namespace CarTime.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EditOrderController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly AppDbContext _context;
        public EditOrderController(DataManager dataManager, AppDbContext context)
        {
            _dataManager = dataManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowOrders(string status)
        {
            return View(_dataManager.Orders.GetOrdersByStatus(status));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View(_context.Orders.FirstOrDefault(o => o.Id == id));
        }
        [HttpPost]
        public IActionResult Edit(Guid id, string status)
        {
            Order order = new Order();
            order = _context.Orders.FirstOrDefault(o => o.Id == id);
            order.Status = status;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(EditOrderController.Index),
                nameof(EditOrderController).CutController());
        }
    }
}
