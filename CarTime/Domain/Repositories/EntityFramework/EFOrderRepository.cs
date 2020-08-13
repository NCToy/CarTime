using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Domain.Repositories.Abstract;
using CarTime.Models;

namespace CarTime.Domain.Repositories.EntityFramework
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        private readonly ShopCart shopCart;

        public EFOrderRepository(AppDbContext context, ShopCart shopCart)
        {
            this.context = context;
            this.shopCart = shopCart;
        }

        public Order GetOrder(string id)
        {
            return context.Orders.FirstOrDefault(o => o.Id.ToString() == id);
        }

        public IQueryable<Order> GetOrdersByStatus(string status)
        {
            return context.Orders.Where(o => o.Status == status);
        }

        public IQueryable<Order> GetOrdersByUser(UserData userData)
        {
            return context.Orders.Where(o => o.UserDataId.ToString() == userData.Id);
        }

        public void CreateOrder(Order order)
        {
            context.Orders.Add(order);

            var items = shopCart.ListShopItems;
            foreach(var el in items)
            {
                var orderDetail = new OrderDetail
                {
                    CarItemId = el.CarItem.Id,
                    OrderId = order.Id,
                    Price = el.CarItem.Price
                };

                context.OrderDetails.Add(orderDetail);
            }

            context.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            context.Orders.Remove(new Order { Id = id });
            context.SaveChanges();
        }
    }
}
