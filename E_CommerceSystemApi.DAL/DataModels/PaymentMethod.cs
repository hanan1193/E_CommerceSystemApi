using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.DAL.Models;

namespace E_CommerceSystemApi.DAL.DataModels
{
     public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string MethodName { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
