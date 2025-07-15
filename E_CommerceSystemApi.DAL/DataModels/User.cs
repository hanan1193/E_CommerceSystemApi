using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.DAL.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ShippingAddress { get; set; }

        // Foreign Key
        public int RoleID { get; set; }

        // Navigation Property
        public Role role { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
