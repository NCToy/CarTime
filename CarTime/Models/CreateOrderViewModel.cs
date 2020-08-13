using System;
using System.ComponentModel.DataAnnotations;

namespace CarTime.Models
{
    public class CreateOrderViewModel
    {
        public Guid UserDataId { get; set; }

        public virtual UserData UserData { get; set; }

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
        [MinLength(10, ErrorMessage = "Номер телефона не может содержать менее 10 символов!")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Некорректное значение в поле Email!")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Price")]
        public ulong Price { get; set; }
    }
}
