using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.DAL.DataModels;

namespace E_CommerceSystemApi.DAL.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? ShippedDate { get; set; }
        // Foreign Key
        public int UserID { get; set; }
        public int PaymentMethodID { get; set; }
        // Navigation Property
        public User user { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public PaymentMethod PaymentMethod { get; set; }

    }
}
