using System;
using System.ComponentModel.DataAnnotations;

namespace CarTime.Domain.Entities
{
    public class OrderDetail
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public Guid CarItemId { get; set; }
        public virtual CarItem CarItem { get; set; }

        [Required]
        public ulong Price { get; set; }
    }
}
