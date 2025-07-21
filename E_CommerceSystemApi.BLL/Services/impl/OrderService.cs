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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        // Constructor injection for the repository and mapper
        public OrderService(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> AddOrder(OrderViewModel orderViewModel)
        {
            try
            {
                var order = _mapper.Map<Order>(orderViewModel);
                await _orderRepository.Add(order);
                return _mapper.Map<OrderViewModel>(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddOrder: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);
                if (order == null)
                {
                    return false;
                }
                await _orderRepository.Delete(id);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting order: {ex.Message}");
                return false;
            }

        }

        public async Task<IEnumerable<OrderViewModel>> GetOrders()
        {
            // Retrive all Orders from the repository and map them to ViewModel
            var orders = await _orderRepository.GetAll();
            return _mapper.Map<IEnumerable<OrderViewModel>>(orders);
        }

        public async Task<OrderViewModel> GetOrderById(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);
                if (order == null)
                    return null;

                var orderViewModel = _mapper.Map<OrderViewModel>(order);
                return orderViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while retrieving Order with ID {id}: {ex.Message}");
                return null;

            }

        }

        public async Task<bool> UpdateOrder(OrderViewModel orderViewModel)
        {
            try
            {
                var existingOrder = await _orderRepository.GetById(orderViewModel.OrderID);
                if (existingOrder == null)
                    return false;
                _mapper.Map(orderViewModel, existingOrder);

                await _orderRepository.Update(existingOrder);
                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating order: {ex.Message}");
                return false;

            }
        }
    }
}

