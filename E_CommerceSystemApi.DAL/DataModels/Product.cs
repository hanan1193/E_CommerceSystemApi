using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.DAL.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Foreign Key
        public int CategoryID { get; set; }

        // Navigation Property
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
