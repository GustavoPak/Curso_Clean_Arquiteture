using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategoryById(int ? id);
        Task AddCategory(CategoryDTO categoryDto);
        Task UpdateCategory(CategoryDTO categoryDto);
        Task DeleteCategory(int ? id);

    }
}
