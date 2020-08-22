using System;

namespace CarTime.Domain.Entities
{
    public class ShopCartItem
    {
        public Guid Id { get; set; }

        public CarItem CarItem { get; set; }

        public double price { get; set; }
        
        public string ShopCartId { get; set; }
    }
}
