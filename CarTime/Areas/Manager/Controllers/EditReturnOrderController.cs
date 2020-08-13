using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarTime.Areas.Admin.Controllers;
using CarTime.Domain;
using CarTime.Models;
using CarTime.Service;

namespace CarTime.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EditReturnOrderController : Controller
    {
        private readonly AppDbContext _context;

        public EditReturnOrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowOrders(string status)
        {
            return View(_context.ReturnOrders.Where(ro => ro.Status == status));
        }

        public IActionResult OrderDetail(Guid id)
        {
            return View(_context.ReturnOrders.FirstOrDefault(ro => ro.Id == id));
        }
        [HttpPost]
        public IActionResult OrderDetail(Guid id, string status)
        {
            ReturnOrder returnOrder = new ReturnOrder();
            returnOrder = _context.ReturnOrders.FirstOrDefault(ro => ro.Id == id);
            returnOrder.Status = status;
            _context.ReturnOrders.Update(returnOrder);
            _context.SaveChanges();
            return RedirectToAction(nameof(EditOrderController.Index), nameof(EditOrderController).CutController());
        }
    }
}
