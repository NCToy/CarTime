using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTime.Domain.Entities
{
    public class BrandItem
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image")] 
        public string TitleImagePath { get; set; }

        public DateTime DateAdding { get; set; }

        public List<CarItem> CarItems { get; set; }
    }
}
