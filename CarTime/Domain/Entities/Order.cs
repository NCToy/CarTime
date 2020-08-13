using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTime.Models;

namespace CarTime.Domain.Entities
{
    public class Order
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserDataId { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Номер телефона не може содержать менее 10 символов!")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Некорректное значение в поле Email!")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display (Name = "Price")]
        public ulong Price { get; set; }

        public string Status { get; set; }

        public DateTime DateAdding { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
