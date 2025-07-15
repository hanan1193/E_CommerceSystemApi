using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.DAL.Repository.intf;
using E_CommerceSystemApi.DAL.Models;
using AutoMapper;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.BLL.Services.intf;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystemApi.BLL.Services.impl
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task AddRole(RoleViewModel roleViewModel)
        {
            var role = _mapper.Map<Role>(roleViewModel);
            await _roleRepository.Add(role);
        }
        //public async Task DeleteRole(int id)
        //{
        //    await _roleRepository.Delete(id);
        //}
        public async Task<bool> DeleteRole(int id)
        {
            var role = await _roleRepository.GetById(id);
            if (role == null)
            {
                return false;
            }

            await _roleRepository.Delete(id);
            return true;
        }
        //public async Task DeleteRole(int id)
        //{
        //    var existingRole = await _roleRepository.GetById(id);
        //    if (existingRole == null)
        //        throw new Exception("User not found.");

        //    await _roleRepository.Delete(id);
        //}
        public async Task<RoleViewModel> GetRoleById(int id)
        {
            var role = await _roleRepository.GetById(id);
            return _mapper.Map<RoleViewModel>(role);
        }
        public async Task<IEnumerable<RoleViewModel>> GetRoles()
        {
            var roles = await _roleRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
        }
        public async Task UpdateRole(RoleViewModel roleViewModel)
        {
            var role = _mapper.Map<Role>(roleViewModel);
            await _roleRepository.Update(role);
        }
    }
}
