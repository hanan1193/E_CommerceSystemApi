using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;

namespace E_CommerceSystemApi.BLL.Services.intf
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodViewModel>> GetPaymentMethods();
        Task<PaymentMethodViewModel> GetPaymentMethodById(int id);
        Task AddPaymentMethod(PaymentMethodViewModel paymentMethod);
        Task<bool> DeletePaymentMethod(int id);
        Task<bool> UpdatePaymentMethod(PaymentMethodViewModel paymentMethod);
    }
}
