using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }
        public string Description { get; set; }
        public string Imageurl { get; set; }
        public string CatagoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
