using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.DataModels;
using E_CommerceSystemApi.DAL.Models;

namespace E_CommerceSystemApi.BLL.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserCreateViewModel>().ReverseMap();
            CreateMap<User, UserUpdateViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }
    }
}
