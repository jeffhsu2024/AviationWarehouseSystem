using DataAccess.Data;
using DataAccess.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AviationWarehouseSystem.Controllers
{
    public class DutyFreeProductsController : Controller
    {
        private readonly IDutyFreeProductService _productService;
        private readonly WarehouseContext _context;
        public DutyFreeProductsController(IDutyFreeProductService productService, WarehouseContext context)
        {
            _productService = productService;
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<DutyFreeProduct> products;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = await _productService.SearchProductsAsync(searchString);
                ViewData["CurrentFilter"] = searchString;
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            return View(products);
        }

        //GET: DutyFreeProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");
            return View();
        }
        // POST: DutyFreeProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DutyFreeProduct product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                TempData["SuccessMessage"] = "商品建立成功！";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", product.SupplierId);
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", product.SupplierId);
            return View(product);
        }

        // POST: DutyFreeProducts/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DutyFreeProduct product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                TempData["SuccessMessage"] = "商品更新成功！";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", product.SupplierId);
            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        // POST: DutyFreeProducts/Delete/2
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
