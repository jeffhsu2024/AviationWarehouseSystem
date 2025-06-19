using DataAccess.IService;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AviationWarehouseSystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<DutyFreeProduct> products;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = await _unitOfWork.DutyFreeProduct.SearchProductsAsync(searchString);
                ViewData["CurrentFilter"] = searchString;
            }
            else
            {
                products = await _unitOfWork.DutyFreeProduct.GetAllProductsAsync();
            }

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _unitOfWork.DutyFreeProduct.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}