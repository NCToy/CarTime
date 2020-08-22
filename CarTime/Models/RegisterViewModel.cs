using System;
using System.ComponentModel.DataAnnotations;

namespace CarTime.Models
{
    public class RegisterViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        
        [Required]
        [UIHint("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
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
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "BirthDay")]
        public DateTime BirthDay { get; set; }
        
        [Display(Name = "DriverPath")]
        public string DriverPath { get; set; }
        
        public DateTime DateAdded = DateTime.UtcNow;
    }
}
