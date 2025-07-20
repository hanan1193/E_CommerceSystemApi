using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.BLL.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? ShippedDate { get; set; }

        // IDs
        public int UserID { get; set; }
        public int PaymentMethodID { get; set; }
    }
}
