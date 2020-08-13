using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Models;
using CarTime.Service;
using System;
using System.Linq;

namespace CarTime.Areas.User.Controllers
{
    [Area("User")]
    public class UserOrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DataManager _dataManager;
        private readonly ShopCart _shopCart;
        private readonly UserManager<UserData> _userManager;

        public UserOrderController(AppDbContext context, ShopCart shopCart, DataManager dataManager, UserManager<UserData> userManager)
        {
            _shopCart = shopCart;
            _context = context;
            _dataManager = dataManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            _shopCart.ListShopItems = _shopCart.getShopItems();
            if (_shopCart.ListShopItems.Count == 0)
            {
                ModelState.AddModelError("", "У вас должны быть товары в корзине!");
                return RedirectToAction("Index", "ShopCart");
            }

            CreateOrderViewModel order = new CreateOrderViewModel();

            order.Price = _shopCart.cPrice(_shopCart.getShopItems());

            string userId = HttpContext.Session.GetString("UserId");

            UserData user = new UserData();
            user = _context.UserData.FirstOrDefault(u => u.Id == userId);

            order.UserDataId = new Guid(user.Id);
            order.Name = user.Name;
            order.Surname = user.Surname;
            order.Patronymic = user.Patronymic;
            order.PhoneNumber = user.PhoneNumber;
            order.Email = user.Email;

            return View(order);
        }
        [HttpPost]
        public IActionResult Index(CreateOrderViewModel order)
        {
            _shopCart.ListShopItems = _shopCart.getShopItems();
            if (ModelState.IsValid)
            {
                Order newOrder = new Order();

                newOrder.Id = Guid.NewGuid();
                string userId = HttpContext.Session.GetString("UserId");
                newOrder.UserDataId = new Guid(userId);
                newOrder.Name = order.Name;
                newOrder.Surname = order.Surname;
                newOrder.Patronymic = order.Patronymic;
                newOrder.PhoneNumber = order.PhoneNumber;
                newOrder.Email = order.Email;
                newOrder.Address = order.Address;
                newOrder.Price = order.Price;
                newOrder.DateAdding = DateTime.UtcNow;
                newOrder.Status = "created";

                _dataManager.Orders.CreateOrder(newOrder);

                return RedirectToAction("Thanks", "UserMain");
            }
            return View(order);
        }
    }
}
