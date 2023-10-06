﻿using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand,Product>
    {
        private readonly IproductRepository _productRepository;

        public ProductRemoveCommandHandler(IproductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException();
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.Id);
        }
    }
}
