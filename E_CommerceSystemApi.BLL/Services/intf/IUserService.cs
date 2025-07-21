using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.Models;


namespace E_CommerceSystemApi.BLL.Services.intf
{
    public interface IUserService
    {
        //Task<IEnumerable<User>> GetUsers();
        //Task<User> GetUserById(int id);
        //Task AddUser(User user);
        //Task DeleteUser(int id);
        //Task UpdateUser(User user);
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUserById(int id);
        Task<UserViewModel> AddUser(UserCreateViewModel user);
        Task DeleteUser(int id);
        Task UpdateUser(UserUpdateViewModel user);
        // Authentication method to verify user credentials
        User? Authenticate(string email, string password);

    }
}
