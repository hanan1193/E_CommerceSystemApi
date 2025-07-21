using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;

namespace E_CommerceSystemApi.BLL.Services.intf
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemViewModel>> GetOrderItems();
        Task<OrderItemViewModel> GetOrderItemById(int id);
        Task<OrderItemViewModel> AddOrderItem(OrderItemViewModel orderItem);
        Task<bool> DeleteOrderItem(int id);
        Task<bool> UpdateOrderItem(OrderItemViewModel orderItem);
    }
}
