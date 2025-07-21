using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.DAL.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public decimal TotalPrice { get; set; }
        // Foreign Key
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        // Navigation Properties
        public Order order { get; set; }
        public Product product { get; set; }
    }
}
