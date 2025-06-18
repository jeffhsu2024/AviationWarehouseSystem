using DataAccess;
using DataAccess.Data;
using DataAccess.IService;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AviationWarehouseSystem.Controllers
{
    public class ProductCategoryController : Controller
    {
        //private readonly WarehouseContext _context;
        //private readonly IProductCategoryService _categoryService;

        //public ProductCategoryController(WarehouseContext context, IProductCategoryService categoryService)
        //{
        //    _context = context;
        //    _categoryService = categoryService;
        //}
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<ProductCategory> categories;

            if (!string.IsNullOrEmpty(searchString))
            {
                //categories = await _categoryService.SearchProductCategoryAsync(searchString);
                categories = await _unitOfWork.ProductCategory.SearchProductCategoryAsync(searchString);
                ViewData["CurrentFilter"] = searchString;
            }
            else
            {
                //categories = await _categoryService.GetAllProductCategoryAsync();
                categories = await _unitOfWork.ProductCategory.GetAllAsync();
            }

            return View(categories);
        }

        // GET: ProductCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.CreatedDate = DateTime.Now;
                //await _categoryService.CreateProductCategoryAsync(productCategory);
                await _unitOfWork.ProductCategory.CreateAsync(productCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //var category = await _categoryService.GetProductCategoryByIdAsync(id);
            var category = await _unitOfWork.ProductCategory.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: ProductCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //await _categoryService.UpdateProductCategoryAsync(productCategory);
                await _unitOfWork.ProductCategory.UpdateAsync(productCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: ProductCategory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //var category = await _categoryService.GetProductCategoryByIdAsync(id);
            var category = await _unitOfWork.ProductCategory.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //await _categoryService.DeleteProductCategoryAsync(id);
            await _unitOfWork.ProductCategory.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}