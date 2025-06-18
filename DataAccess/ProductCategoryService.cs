using DataAccess.Data;
using DataAccess.IService;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductCategoryService : Service<ProductCategory>, IProductCategoryService
    {
        private readonly WarehouseContext _context;

        public ProductCategoryService(WarehouseContext context) : base(context)
        {
            _context = context;
        }

        // 僅回傳啟用的分類
        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoryAsync()
        {
            return await _context.ProductCategories
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        // 取得單一分類
        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // 建立分類
        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        // 更新分類
        public async Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        // 軟刪除分類
        public async Task<bool> DeleteProductCategoryAsync(int id)
        {
            var category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return false;

            category.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // 搜尋分類
        public async Task<IEnumerable<ProductCategory>> SearchProductCategoryAsync(string searchTerm)
        {
            return await _context.ProductCategories
                .Where(p => p.IsActive &&
                            (p.CategoryName.Contains(searchTerm) ||
                             p.CategoryCode.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}