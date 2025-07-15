using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceSystemApi.BLL.ViewModels
{
    public class UserUpdateViewModel
    {
       
            public int UserID { get; set; } // Required to identify which user to update
            public string Name { get; set; }
            public string Email { get; set; }
            public string ShippingAddress { get; set; }
            public int RoleID { get; set; }
            public string? Password { get; set; } // Optional: Only update password if provided
    }
}
