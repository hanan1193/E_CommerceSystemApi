using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.DataModels;
using E_CommerceSystemApi.DAL.Models;
using E_CommerceSystemApi.DAL.Repository.intf;

namespace E_CommerceSystemApi.BLL.Services.impl
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IRepository<PaymentMethod> _paymentMethodRepository;
        private readonly IMapper _mapper;

        public PaymentMethodService(IRepository<PaymentMethod> paymentMethodRepository,IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;

        }
        public async Task<PaymentMethodViewModel> AddPaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodViewModel);
                await _paymentMethodRepository.Add(paymentMethod);
                return _mapper.Map<PaymentMethodViewModel>(paymentMethod);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddPaymentMethod: {ex.Message}");
                return null;
            }

        }

        public async Task<bool> DeletePaymentMethod(int id)
        {
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetById(id);
                if (paymentMethod == null)
                {
                    return false;
                }
                await _paymentMethodRepository.Delete(id);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting paymentMethod {ex.Message}");
                return false;
            }
        }

        public async Task<PaymentMethodViewModel> GetPaymentMethodById(int id)
        {
            try
            {
                var paymentMethod = await _paymentMethodRepository.GetById(id);
                if (paymentMethod == null)
                    return null;

                var paymentMethodViewModel = _mapper.Map<PaymentMethodViewModel>(paymentMethod);
                return paymentMethodViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while retrieving paymentMethod with ID {id}: {ex.Message}");
                return null;

            }
        }

        public async Task<IEnumerable<PaymentMethodViewModel>> GetPaymentMethods()
        {
            // Retrive all PaymentMethods from the repository and map them to ViewModel
            var paymentMethods = await _paymentMethodRepository.GetAll();
            return _mapper.Map<IEnumerable<PaymentMethodViewModel>>(paymentMethods);
        }

        public async Task<bool> UpdatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                var existingPaymentMethod = await _paymentMethodRepository.GetById(paymentMethodViewModel.PaymentMethodID);
                if (existingPaymentMethod == null)
                    return false;
                _mapper.Map(paymentMethodViewModel, existingPaymentMethod);

                await _paymentMethodRepository.Update(existingPaymentMethod);
                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating paymentMethod: {ex.Message}");
                return false;

            }
        }
    }
}
