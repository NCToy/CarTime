using CarTime.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarTime.Models
{
    public class ReturnOrder
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "OrderId")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

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
        [MinLength(10, ErrorMessage = "Номер телефона не должен содержать менее 10 символов!")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Некорректное значение в поле Email!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Опишите причину возврата на более чем 10 символов!")]
        [Display(Name = "Reason")]
        public string Reason { get; set; }

        public DateTime DateAdding { get; set; }

        public string Status { get; set; }
    }
}
