using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IproductRepository _productRepository;

        public ProductUpdateCommandHandler(IproductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if(product == null)
            {
                throw new ApplicationException($"Application cound be Updated");
            }
            else
            {
                var ProductUpdate = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image,request.CategoryId);

                product.Update(ProductUpdate);

                var result = await _productRepository.UpadateAsync(product);

                return result;
            }
        }
    }
}
