using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;

namespace E_CommerceSystemApi.BLL.Services.intf
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
        Task<ProductViewModel> GetProductById(int id);
        Task AddProduct(ProductViewModel product);
        Task<bool> DeleteProduct(int id);
        Task UpdateProduct(ProductViewModel product);

    }
}
