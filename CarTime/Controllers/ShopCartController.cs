using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Models;

namespace CarTime.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ShopCart _shopCart;
        private readonly AppDbContext _context;

        public ShopCartController(ShopCart shopCart, AppDbContext context)
        {
            _shopCart = shopCart;
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _shopCart.getShopItems();
            _shopCart.ListShopItems = items;
      
            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart,
                Price = _shopCart.cPrice(items)
            };
            
            return View(obj);
        }

        [HttpGet]
        public IActionResult DeleteFromCart(Guid shopCartItemId)
        {
            var item = _context.ShopCartItems.FirstOrDefault(i => i.Id == shopCartItemId);
            _shopCart.DeleteFromCart(item);
            return RedirectToAction("Index", "ShopCart");
        }
    }
}
