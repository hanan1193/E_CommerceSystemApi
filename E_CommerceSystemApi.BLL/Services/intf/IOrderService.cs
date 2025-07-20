using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;

namespace E_CommerceSystemApi.BLL.Services.intf
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetOrders();
        Task<OrderViewModel> GetOrderById(int id);
        Task<OrderViewModel> AddOrder(OrderViewModel order);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrder(OrderViewModel order);
    }
}
