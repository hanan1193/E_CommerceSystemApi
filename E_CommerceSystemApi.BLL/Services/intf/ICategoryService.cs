using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceSystemApi.BLL.ViewModels;

namespace E_CommerceSystemApi.BLL.Services.intf
{
     public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetCategories();
        Task<CategoryViewModel> GetCategoryById(int id);
        Task<CategoryViewModel> AddCategory(CategoryViewModel category);
        Task<bool> DeleteCategory(int id);
        Task <bool>UpdateCategory(CategoryViewModel category);
    }
}
