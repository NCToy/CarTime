using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarTime.Models
{
    public class UserData : IdentityUser
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        public DateTime DateAdded = DateTime.UtcNow;
    }
}
