using System;
using System.Linq;
using CarTime.Domain.Entities;
using CarTime.Models;

namespace CarTime.Domain.Repositories.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrdersByUser(UserData userData);
        IQueryable<Order> GetOrdersByStatus(string status);        
        Order GetOrder(string id);
        void CreateOrder(Order order);
        void DeleteOrder(Guid id);
    }
}