using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHost;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,ICategoryService categoryService, IWebHostEnvironment webHost)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            try
            {
                await _productService.Add(product);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                throw;
            }
        }

       public async Task<IActionResult> Edit(int ? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var product = await _productService.GetById(id);

            if(product == null)
            {
                return BadRequest();
            }

            ViewBag.CategoryId =
                new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId =
                new SelectList(await _categoryService.GetCategories(), "Id", "Name");
                return View(product);
            }

            await _productService.Update(product);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int ? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var product = await _productService.GetById(id);

            if(product == null)
            {
                return BadRequest();
            }

            return View(product);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int ? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            await _productService.Remove(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int ? id)
        {
            if (id == null) return BadRequest();

            var product = await _productService.GetById(id);

            if (product == null) return NotFound();

            var WebRootPatch = _webHost.WebRootPath;

            var ImagePatch = Path.Combine(WebRootPatch, "images//", product.Image);
            var Image = System.IO.File.Exists(ImagePatch);

            ViewBag.ImageExist = Image;

            return View(product);
        }
         
    }
}
