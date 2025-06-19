using DataAccess.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using AviationWarehouseSystem.Extensions;

namespace AviationWarehouseSystem.Controllers
{
    public class TaxableGoodsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaxableGoodsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<TaxableGoods> goods;

            if (!string.IsNullOrEmpty(searchString))
            {
                goods = await _unitOfWork.TaxableGoods.SearchAsync(searchString);
                ViewData["CurrentFilter"] = searchString;
            }
            else
            {
                goods = await _unitOfWork.TaxableGoods.GetAllWithDetailsAsync();
            }

            return View(goods);
        }

        public async Task<IActionResult> Details(int id)
        {
            var goods = await _unitOfWork.TaxableGoods.GetByIdWithDetailsAsync(id);
            if (goods == null) return NotFound();

            return View(goods);
        }

        public async Task<IActionResult> Create()
        {
            // 準備下拉選單資料
            var taxCategories = await _unitOfWork.TaxCategory.GetAllAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync();
            var locations = await _unitOfWork.StorageLocation.GetAllAsync();

            ViewData["TaxCategoryId"] = new SelectList(taxCategories, "Id", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName");
            ViewData["StorageLocationId"] = new SelectList(locations, "Id", "LocationName");

            // 準備報關狀態下拉選單
            ViewData["CustomsStatus"] = new SelectList(Enum.GetValues(typeof(CustomsStatus))
                .Cast<CustomsStatus>()
                .Select(e => new {
                    Value = (int)e,
                    Text = e.GetDisplayName()
                }), "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxableGoods goods)
        {
            if (ModelState.IsValid)
            {
                // 只有在驗證通過時才載入導航屬性
                if (goods.SupplierId.HasValue)
                    goods.Supplier = await _unitOfWork.Supplier.GetByIdAsync(goods.SupplierId.Value);
                if (goods.TaxCategoryId.HasValue)
                    goods.TaxCategory = await _unitOfWork.TaxCategory.GetByIdAsync(goods.TaxCategoryId.Value);
                if (goods.StorageLocationId.HasValue)
                    goods.StorageLocation = await _unitOfWork.StorageLocation.GetByIdAsync(goods.StorageLocationId.Value);

                goods.CreatedDate = DateTime.Now;
                await _unitOfWork.TaxableGoods.CreateAsync(goods);
                TempData["SuccessMessage"] = "報稅貨物建立成功！";
                return RedirectToAction(nameof(Index));
            }

            // 驗證失敗時重新準備下拉選單資料並保持選中值
            var taxCategories = await _unitOfWork.TaxCategory.GetAllAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync();
            var locations = await _unitOfWork.StorageLocation.GetAllAsync();

            ViewData["TaxCategoryId"] = new SelectList(taxCategories, "Id", "CategoryName", goods.TaxCategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName", goods.SupplierId);
            ViewData["StorageLocationId"] = new SelectList(locations, "Id", "LocationName", goods.StorageLocationId);

            ViewData["CustomsStatus"] = new SelectList(Enum.GetValues(typeof(CustomsStatus))
                .Cast<CustomsStatus>()
                .Select(e => new {
                    Value = (int)e,
                    Text = e.GetDisplayName()
                }), "Value", "Text", (int)goods.CustomsStatus);

            return View(goods);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var goods = await _unitOfWork.TaxableGoods.GetByIdWithDetailsAsync(id);
            if (goods == null) return NotFound();

            // 準備下拉選單資料並設定選中值
            var taxCategories = await _unitOfWork.TaxCategory.GetAllAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync();
            var locations = await _unitOfWork.StorageLocation.GetAllAsync();

            ViewData["TaxCategoryId"] = new SelectList(taxCategories, "Id", "CategoryName", goods.TaxCategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName", goods.SupplierId);
            ViewData["StorageLocationId"] = new SelectList(locations, "Id", "LocationName", goods.StorageLocationId);

            // 準備報關狀態下拉選單
            ViewData["CustomsStatus"] = new SelectList(Enum.GetValues(typeof(CustomsStatus))
                .Cast<CustomsStatus>()
                .Select(e => new {
                    Value = (int)e,
                    Text = e.GetDisplayName()
                }), "Value", "Text", (int)goods.CustomsStatus);

            return View(goods);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaxableGoods goods)
        {
            if (id != goods.Id) return NotFound();

            if (ModelState.IsValid)
            {
                goods.UpdatedDate = DateTime.Now;
                await _unitOfWork.TaxableGoods.UpdateAsync(goods);
                TempData["SuccessMessage"] = "報稅貨物更新成功！";
                return RedirectToAction(nameof(Index));
            }

            // 驗證失敗時重新準備下拉選單資料並保持選中值
            var taxCategories = await _unitOfWork.TaxCategory.GetAllAsync();
            var suppliers = await _unitOfWork.Supplier.GetAllAsync();
            var locations = await _unitOfWork.StorageLocation.GetAllAsync();

            ViewData["TaxCategoryId"] = new SelectList(taxCategories, "Id", "CategoryName", goods.TaxCategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "Id", "SupplierName", goods.SupplierId);
            ViewData["StorageLocationId"] = new SelectList(locations, "Id", "LocationName", goods.StorageLocationId);

            // 準備報關狀態下拉選單
            ViewData["CustomsStatus"] = new SelectList(Enum.GetValues(typeof(CustomsStatus))
                .Cast<CustomsStatus>()
                .Select(e => new {
                    Value = (int)e,
                    Text = e.GetDisplayName()
                }), "Value", "Text", (int)goods.CustomsStatus);

            return View(goods);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var goods = await _unitOfWork.TaxableGoods.GetByIdWithDetailsAsync(id);
            if (goods == null) return NotFound();

            return View(goods);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.TaxableGoods.DeleteAsync(id);
            TempData["SuccessMessage"] = "報稅貨物刪除成功！";
            return RedirectToAction(nameof(Index));
        }


    }
}