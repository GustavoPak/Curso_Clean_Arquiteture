﻿using CleanArchMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IproductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetByIdAsync(int? id);
        //Task<Product> GetProductCategoryAsync(int? id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpadateAsync(Product product);
        Task<Product> DeleteAsync(Product product);
    }
}
