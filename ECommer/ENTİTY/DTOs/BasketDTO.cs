using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTİTY.DTOs
{
    public class BasketDTO
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }
    }
}
