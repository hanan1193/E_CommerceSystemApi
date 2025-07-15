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
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        // Constructor injection for the repository and mapper
        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task  AddCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryViewModel);
                await _categoryRepository.Add(category);
            }
            catch(Exception ex){
                Console.WriteLine($"Error in AddCategory: {ex.Message}");
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if (category==null)
                {
                    return false;     
                }
                await _categoryRepository.Delete(id);
                return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while deleting category: {ex.Message}");
                return false;
            }
            
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            // Retrive all categories from the repository and map them to ViewModel
            var categories = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                if (category == null)
                    return null;

                var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
                return categoryViewModel;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while retrieving category with ID {id}: {ex.Message}");
                return null;

            }

        }

        public async Task<bool> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetById(categoryViewModel.Id);
                if (existingCategory == null)
                    return false;
                _mapper.Map(categoryViewModel, existingCategory);

                await _categoryRepository.Update(existingCategory);
                return true;


            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while updating category: {ex.Message}");
                return false;

            }
        }
    }
}
