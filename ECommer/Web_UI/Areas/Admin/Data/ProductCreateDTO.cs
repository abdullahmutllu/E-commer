using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_UI.Areas.Admin.Data
{
    public class ProductAdminCreateDTO
    {
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public int Stok { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public IFormFile[] ProductImagies { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public int[] CatgoryId { get; set; }
    }
}
