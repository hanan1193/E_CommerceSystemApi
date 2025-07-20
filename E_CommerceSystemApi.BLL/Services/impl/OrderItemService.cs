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
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        public OrderItemService(IRepository<OrderItem> orderItemRepository,IMapper mapper)

        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }
        public async Task<OrderItemViewModel> AddOrderItem(OrderItemViewModel orderItemViewModel)
        {
            try
            {
                var order = _mapper.Map<OrderItem>(orderItemViewModel);
                _orderItemRepository.Add(order);
                return _mapper.Map<OrderItemViewModel>(order);

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erroe in AddOrderItem:{ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteOrderItem(int id)
        {
            try
            {
                // check if the order item exists
                var orderItem = await _orderItemRepository.GetById(id);
                if(orderItem==null)
                {
                    return false;
                }
                await _orderItemRepository.Delete(id);
                return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while deleting order item :{ex.Message}");
                return false;
            }
        }

        public async Task<OrderItemViewModel> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetById(id);
                if(orderItem ==null)
                {
                    throw new Exception("Order item not found.");
                }
                var orderItemViewModel = _mapper.Map<OrderItemViewModel>(orderItem);
                return orderItemViewModel;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in retrieving orderItem with ID {id}: {ex.Message}");
                return null;
            }
           

           
        }

        public async Task<IEnumerable<OrderItemViewModel>> GetOrderItems()
        {
            // Retrive all orderItems from the repository and map them to ViewModel
            var orderItems = await _orderItemRepository.GetAll();
            return _mapper.Map<IEnumerable<OrderItemViewModel>>(orderItems);
        }

        public async Task<bool> UpdateOrderItem(OrderItemViewModel orderItemViewModel)
        {
            try
            {
                var existingOrderItem = await _orderItemRepository.GetById(orderItemViewModel.OrderID);
                if (existingOrderItem == null)
                    return false;
                _mapper.Map(orderItemViewModel, existingOrderItem);

                await _orderItemRepository.Update(existingOrderItem);
                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating OrderItem: {ex.Message}");
                return false;

            }
        }
    }
}
