using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarTime.Domain.Entities
{
    public class CarItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid BrandId { get; set; }
        public virtual BrandItem Brand { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [MinLength(10, ErrorMessage = "Краткое описание не должно содержать менее 10 символов!")]
        [Display(Name = "SubTitle")]
        public string SubTitle { get; set; }

        [MinLength(20, ErrorMessage = "Полное описание не должно содержать менее 20 символов!")]
        [Display(Name = "Description")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string TitleImagePath { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public ulong Price { get; set; }
        
        [Display(Name = "Available")] 
        public bool Available { get; set; }

        public DateTime DateAdding { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
