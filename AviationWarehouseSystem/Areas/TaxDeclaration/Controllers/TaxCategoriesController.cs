using DataAccess.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace AviationWarehouseSystem.Areas.TaxDeclaration.Controllers
{
    [Area("TaxDeclaration")]
    public class TaxCategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaxCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: TaxCategories
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<TaxCategory> categories;

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = await _unitOfWork.TaxCategory.SearchCategoriesAsync(searchString);
                ViewData["CurrentFilter"] = searchString;
            }
            else
            {
                categories = await _unitOfWork.TaxCategory.GetAllActiveCategoriesAsync();
            }

            return View(categories);
        }

        // GET: TaxCategories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _unitOfWork.TaxCategory.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: TaxCategories/Create
        public async Task<IActionResult> Create()
        {
            await PrepareViewBagSelectLists();
            return View();
        }

        // POST: TaxCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxCategory taxCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    taxCategory.CreatedDate = DateTime.Now;
                    await _unitOfWork.TaxCategory.CreateAsync(taxCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "稅則分類建立成功！";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = "建立稅則分類時發生錯誤：" + ex.Message;
                }
            }

            await PrepareViewBagSelectLists(taxCategory.ParentCategoryId);
            return View(taxCategory);
        }

        // GET: TaxCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _unitOfWork.TaxCategory.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await PrepareViewBagSelectLists(category.ParentCategoryId);
            return View(category);
        }

        // POST: TaxCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaxCategory taxCategory)
        {
            if (id != taxCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taxCategory.UpdatedDate = DateTime.Now;
                    await _unitOfWork.TaxCategory.UpdateAsync(taxCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "稅則分類更新成功！";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = "更新稅則分類時發生錯誤：" + ex.Message;
                }
            }

            await PrepareViewBagSelectLists(taxCategory.ParentCategoryId);
            return View(taxCategory);
        }

        // GET: TaxCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.TaxCategory.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: TaxCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var category = await _unitOfWork.TaxCategory.GetByIdAsync(id);
                if (category != null)
                {
                    // 檢查是否有子分類
                    var subCategories = await _unitOfWork.TaxCategory.GetSubCategoriesAsync(id);
                    if (subCategories.Any())
                    {
                        TempData["error"] = "無法刪除此分類，因為它包含子分類。請先刪除所有子分類。";
                        return RedirectToAction(nameof(Index));
                    }

                    // 軟刪除：設定為非啟用狀態
                    category.IsActive = false;
                    category.UpdatedDate = DateTime.Now;
                    await _unitOfWork.TaxCategory.UpdateAsync(category);
                    _unitOfWork.Save();
                    TempData["success"] = "稅則分類刪除成功！";
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "刪除稅則分類時發生錯誤：" + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // 準備下拉選單資料
        private async Task PrepareViewBagSelectLists(int? selectedParentId = null)
        {
            var parentCategories = await _unitOfWork.TaxCategory.GetAllActiveCategoriesAsync();
            ViewBag.ParentCategoryId = new SelectList(
                parentCategories.Where(c => c.ParentCategoryId == null), 
                "Id", 
                "CategoryName", 
                selectedParentId
            );
        }

        // AJAX: 取得子分類
        [HttpGet]
        public async Task<JsonResult> GetSubCategories(int parentId)
        {
            var subCategories = await _unitOfWork.TaxCategory.GetSubCategoriesAsync(parentId);
            var result = subCategories.Select(c => new { 
                id = c.Id, 
                name = c.CategoryName,
                code = c.CategoryCode,
                taxRate = c.TaxRate
            });
            return Json(result);
        }

        // AJAX: 檢查分類代碼是否重複
        [HttpGet]
        public async Task<JsonResult> CheckCategoryCode(string categoryCode, int? id = null)
        {
            var categories = await _unitOfWork.TaxCategory.GetAllAsync();
            var exists = categories.Any(c => c.CategoryCode == categoryCode && c.Id != id);
            return Json(!exists);
        }
    }
}
