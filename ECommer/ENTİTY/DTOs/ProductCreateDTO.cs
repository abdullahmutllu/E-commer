using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
