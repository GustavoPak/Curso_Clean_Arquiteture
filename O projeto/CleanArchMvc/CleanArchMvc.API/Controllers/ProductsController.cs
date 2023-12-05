using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            IEnumerable<ProductDTO> products = await _productService.GetProducts();

            if (products == null)
            {
                return NotFound("Erro do servidor ao processar solicitação.");
            }

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null) return NotFound("Product with this Id is not exist!");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productSource)
        {
            if (productSource == null) return BadRequest("invalid Data!");

            await _productService.Add(productSource);

            return new CreatedAtRouteResult("GetProduct", new { id = productSource.Id }, productSource);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody] ProductDTO productSource)
        {
            if (id != productSource.Id)
            {
                return BadRequest("Product have to has the same value.");
            }

            if (productSource == null)
            {
                return BadRequest("Product not inserted.");
            }

            await _productService.Update(productSource);

            return Ok(productSource);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return BadRequest("Product not found!");
            }

            await _productService.Remove(id);

            return Ok(product);
        }

    }
}
