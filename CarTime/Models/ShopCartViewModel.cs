using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTime.Domain.Entities;

namespace CarTime.Models
{
    public class ShopCartViewModel
    {
        public ShopCart shopCart { get; set; }
        public ulong Price { get; set; }
    }
}
