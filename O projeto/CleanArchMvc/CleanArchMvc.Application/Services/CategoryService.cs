using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategory(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task DeleteCategory(int? id)
        {
            var categoryExist = await _categoryRepository.GetByIdAsync(id);

            await _categoryRepository.DeleteAsync(categoryExist);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetAsync();

            var categoriasDTO = _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);

            return categoriasDTO;
        }

        public async Task<CategoryDTO> GetCategoryById(int? id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);

            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task UpdateCategory(CategoryDTO categoryDto)
        {
            var catEntity = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.UpadateAsync(catEntity);
        }
    }
}
