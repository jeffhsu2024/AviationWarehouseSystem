using DataAccess.Data;
using DataAccess.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AviationWarehouseSystem.Controllers
{
    public class DutyFreeProductsController : Controller
    {
        //private readonly IDutyFreeProductService _productService;
        //private readonly WarehouseContext _context;
        //public DutyFreeProductsController(IDutyFreeProductService productService, WarehouseContext context)
        //{
        //    _productService = productService;
        //    _context = context;
        //}
        private readonly IUnitOfWork _unitOfWork;
        public DutyFreeProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<DutyFreeProduct> products;

            if (!string.IsNullOrEmpty(searchString))
            {
                //products = await _productService.SearchProductsAsync(searchString);
                products = await _unitOfWork.DutyFreeProduct.SearchAsync(searchString);
                ViewData["CurrentFilter"] = searchString;
            }
            else
            {
                //products = await _productService.GetAllProductsAsync();
                products = await _unitOfWork.DutyFreeProduct.GetAllAsync();
            }

            return View(products);
        }

        //GET: DutyFreeProducts/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName");
            //ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName");

            // 取得商品分類與供應商清單
            var categories = await _unitOfWork.ProductCategory.GetAllProductCategoryAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync(); // 這裡應改為供應商服務，假設有 IUnitOfWork.Supplier

            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName");


            return View();
        }
        // POST: DutyFreeProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DutyFreeProduct product)
        {
            if (ModelState.IsValid)
            {
                //await _productService.CreateProductAsync(product);
                await _unitOfWork.DutyFreeProduct.CreateAsync(product);
                TempData["SuccessMessage"] = "商品建立成功！";
                return RedirectToAction(nameof(Index));
            }

            //ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName", product.CategoryId);
            //ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", product.SupplierId);

            // 取得商品分類與供應商清單 
            //Todo : suppliers
            var categories = await _unitOfWork.ProductCategory.GetAllProductCategoryAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync(); // 這裡應改為供應商服務，假設有 IUnitOfWork.Supplier

            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName");

            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            //var product = await _productService.GetProductByIdAsync(id);
            var product = await _unitOfWork.DutyFreeProduct.GetByIdAsync(id);
            if (product == null) return NotFound();

            //ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName", product.CategoryId);
            //ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", product.SupplierId);

            // 取得商品分類與供應商清單 
            //Todo : suppliers
            var categories = await _unitOfWork.ProductCategory.GetAllProductCategoryAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync(); // 這裡應改為供應商服務，假設有 IUnitOfWork.Supplier

            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName");
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
                //await _productService.UpdateProductAsync(product);
                await _unitOfWork.DutyFreeProduct.UpdateAsync(product);
                TempData["SuccessMessage"] = "商品更新成功！";
                return RedirectToAction(nameof(Index));
            }

            //ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "CategoryName", product.CategoryId);
            //ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierName", product.SupplierId);

            // 取得商品分類與供應商清單 
            //Todo : suppliers
            var categories = await _unitOfWork.ProductCategory.GetAllProductCategoryAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync(); // 這裡應改為供應商服務，假設有 IUnitOfWork.Supplier

            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName");

            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var product = await _productService.GetProductByIdAsync(id);
            var product = await _unitOfWork.DutyFreeProduct.GetByIdAsync(id);
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
            //await _productService.DeleteProductAsync(id);
            await _unitOfWork.DutyFreeProduct.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
