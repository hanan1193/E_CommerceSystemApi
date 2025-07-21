using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.BLL.ViewModels
{
    // this class is used to represent the user data in the API responses
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public int RoleID { get; set; }
    }
}
