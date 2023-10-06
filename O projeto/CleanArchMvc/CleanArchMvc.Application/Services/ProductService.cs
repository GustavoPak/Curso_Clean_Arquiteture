using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Handlers;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediaTR;

        public ProductService(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediaTR = mediator;
        }

        public async Task Add(ProductDTO productdto)
        {
            var product = _mapper.Map<ProductCreateCommand>(productdto);

            await _mediaTR.Send(product);
        }

        public async Task<ProductDTO> GetById(int ? id)
        {
            var GetByIdQuery = new GetProductByIdQuery(id.Value);

            if(GetByIdQuery == null)
            {
                throw new Exception("Non fue possible cambiar esta Id.");
            }

            var product = await _mediaTR.Send(GetByIdQuery);

            return _mapper.Map<ProductDTO>(product);
        }

        //public async Task<ProductDTO> GetProductCategory(int ? id)
        //{
        //    var GetByIdQuery = new GetProductByIdQuery(id.Value);

        //    if (GetByIdQuery == null)
        //    {
        //        throw new Exception("Non fue possible cambiar esta Id.");
        //    }

        //    var product = await _mediaTR.Send(GetByIdQuery);

        //    return _mapper.Map<ProductDTO>(product);
        //}

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productQuery = new GetProductsQuery();

            var productEntity = await _mediaTR.Send(productQuery);

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(productEntity);

            return productsDTO;
        }

        public async Task Remove(int ? id)
        {
            var RemoveCommand = new ProductRemoveCommand(id.Value);

            if(RemoveCommand == null)
            {
                throw new Exception("Entity not acepted");
            }

            await _mediaTR.Send(RemoveCommand);
        }

        public async Task Update(ProductDTO productDto)
        {
            var UpdateProduct = _mapper.Map<ProductUpdateCommand>(productDto);

            await _mediaTR.Send(UpdateProduct);
        }
    }
}
