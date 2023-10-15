using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            var categories = await _categoryService.GetCategories();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategory(categoryDto);

                return RedirectToAction("Index");
            }
            else
            {
                return View(categoryDto);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var catDto = await _categoryService.GetCategoryById(id);

            if (catDto == null)
            {
                return NotFound();
            }

            return View(catDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategory(category);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {

                var category = await _categoryService.GetCategoryById(id);

                if(category == null)
                {
                    return NotFound();
                }

                return View(category);

            }
            else
            {
                ViewBag.Error = "Não foi possivel obter essa categoria,erro no servidor";

                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var category = await _categoryService.GetCategoryById(id);

                if (category == null)
                {
                    ViewBag.Error = "Não foi possivel obter essa categoria,erro no servidor";

                    return View("Error");
                }

                return View(category);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int ? id)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
