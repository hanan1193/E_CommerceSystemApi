using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.BLL.ViewModels
{
    // this class is used to receive user data when creating a user
    public class UserCreateViewModel
    {
            public string Name { get; set; }
            public string Email { get; set; }
            public string ShippingAddress { get; set; }
            public int RoleID { get; set; }
            public string Password { get; set; } 
        }
}
