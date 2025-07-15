using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.Models;
using E_CommerceSystemApi.DAL.Repository.intf;

namespace E_CommerceSystemApi.BLL.Services.impl
{
    
        public class UserService : IUserService
        {
            private readonly IRepository<User> _UserRepository;
            private readonly IMapper _mapper;

            public UserService(IRepository<User> userRepository, IMapper mapper)
            {
                _UserRepository = userRepository;
                _mapper = mapper;
            }

            public async Task AddUser(UserCreateViewModel userCreateViewModel)
            {
                var user = _mapper.Map<User>(userCreateViewModel);
                await _UserRepository.Add(user);
            }

            public async Task DeleteUser(int id)
            {
                var existingUser = await _UserRepository.GetById(id);
                if (existingUser == null)
                throw new Exception("User not found.");

                await _UserRepository.Delete(id);
            }

            public async Task<UserViewModel> GetUserById(int id)
            {
                var user = await _UserRepository.GetById(id);
                return _mapper.Map<UserViewModel>(user);
            }

            public async Task<IEnumerable<UserViewModel>> GetUsers()
            {
                var users = await _UserRepository.GetAll();
                return _mapper.Map<IEnumerable<UserViewModel>>(users);
            }

            public async Task UpdateUser(UserUpdateViewModel userUpdateViewModel)
            {
            //var user = _mapper.Map<User>(userCreateViewModel);
            //await _UserRepository.Update(user);
            var existingUser = await _UserRepository.GetById(userUpdateViewModel.UserID);
            if (existingUser == null)
                throw new Exception("User not found.");

            // Update only allowed fields
            existingUser.Name = userUpdateViewModel.Name;
            existingUser.Email = userUpdateViewModel.Email;
            existingUser.ShippingAddress = userUpdateViewModel.ShippingAddress;
            existingUser.RoleID = userUpdateViewModel.RoleID;

            // Optional password update
            if (!string.IsNullOrWhiteSpace(userUpdateViewModel.Password))
            {
                existingUser.Password = userUpdateViewModel.Password;
            }

            await _UserRepository.Update(existingUser);
        }
        }

    }

