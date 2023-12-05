using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            IEnumerable<CategoryDTO> categorys = await _categoryService.GetCategories();

            if (categorys == null)
            {
                return NotFound("Erro do servidor ao processar solicitação.");
            }

            return Ok(categorys);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null) return NotFound("Category with this Id is not exist!");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO categorySource)
        {
            if (categorySource == null) return BadRequest("invalid Data!");

            await _categoryService.AddCategory(categorySource);

            return new CreatedAtRouteResult("GetCategory", new { id = categorySource.Id }, categorySource);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] CategoryDTO categorySource)
        {
            if (id != categorySource.Id)
            {
                return BadRequest("Category have to has the same value.");
            }

            if (categorySource == null)
            {
                return BadRequest("Category not inserted.");
            }

            await _categoryService.UpdateCategory(categorySource);

            return Ok(categorySource);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if(category == null)
            {
                return BadRequest("Category not found!");
            }

            await _categoryService.DeleteCategory(id);

            return Ok(category);
        }

    }
}
