using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CW1_Cloud_MVC_7624.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
