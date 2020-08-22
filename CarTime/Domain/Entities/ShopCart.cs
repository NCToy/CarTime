using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarTime.Models;

namespace CarTime.Domain.Entities
{
    public class ShopCart
    {
        private readonly AppDbContext _context;

        public ShopCart(AppDbContext context)
        {
            this._context = context;
        }

        //*****************************************************

        public string ShopCartId { get; set; }

        public string UserDataId { get; set; }
        public virtual UserData UserData { get; set; }

        public List<ShopCartItem> ListShopItems { get; set; }

        //*****************************************************

        public void DeleteFromCart(ShopCartItem shopCartItem)
        {
            _context.ShopCartItems.Remove(shopCartItem);
            _context.SaveChanges();
        }

        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string shopCartId = session.GetString("ShopCartId") ?? Guid.NewGuid().ToString();

            session.SetString("ShopCartId", shopCartId);

            return new ShopCart(context)
            {
                ShopCartId = shopCartId
            };
        }

        public void AddToCart(CarItem car)
        {
            _context.ShopCartItems.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                CarItem = car,
                price = car.Price
            });

            _context.SaveChanges();
        }

        public List<ShopCartItem> getShopItems()
        {
            return _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.CarItem).ToList();
        }

        public ulong cPrice(List<ShopCartItem> shopCartItems)
        {
            ulong sum = 0;

            foreach(ShopCartItem item in shopCartItems)
            {
                sum += item.CarItem.Price;
            }

            return sum;
        }
    }
}
