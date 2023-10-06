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
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand,Product>
    {
        private readonly IproductRepository _productRepository;

        public ProductCreateCommandHandler(IproductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product(request.Name, request.Description, request.Price,request.Stock,request.Image, request.CategoryId);

            if (product == null)
            {
                throw new ApplicationException($"Error creating Entity.");
            }
            else
            {
                return await _productRepository.CreateAsync(product);
            }
        }
    }
}
