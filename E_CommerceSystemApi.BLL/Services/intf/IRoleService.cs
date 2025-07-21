using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;

namespace E_CommerceSystemApi.BLL.Services.intf
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetRoles();
        Task<RoleViewModel> GetRoleById(int id);
        Task<RoleViewModel> AddRole(RoleViewModel role);
        Task<bool> DeleteRole(int id);
        Task UpdateRole(RoleViewModel role);
    }
}
