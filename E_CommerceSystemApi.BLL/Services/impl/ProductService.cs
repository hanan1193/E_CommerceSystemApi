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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> AddProduct(ProductViewModel productViewModel)
        {
            try
            {
                var product = _mapper.Map<Product>(productViewModel);
                await _productRepository.Add(product);
                return _mapper.Map<ProductViewModel>(product);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in AddProduct:{ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                // check if the product exists
                var product = await _productRepository.GetById(id);
                if (product == null)
                {
                    return false;
                }

                await _productRepository.Delete(id);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while deleting product:{ex.Message}");
                return false;
            }
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            try
            {
                var product = await _productRepository.GetById(id);
                return _mapper.Map<ProductViewModel>(product);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in GetProductById:{ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            try
            {
                var product = _mapper.Map<Product>(productViewModel);
                await _productRepository.Update(product);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in UpdateProduct:{ex.Message}");
            }
        }
    }
}
