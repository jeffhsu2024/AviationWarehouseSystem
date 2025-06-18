using DataAccess.Data;
using DataAccess.IService;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly WarehouseContext _context;
        public ProductCategoryService(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoryAsync()
        {
            return await _context.ProductCategories
                .Where(p => p.IsActive).ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> DeleteProductCategoryAsync(int id)
        {
            var category = _context.ProductCategories.FirstOrDefault(c => c.Id == id);
            if (category == null) return false;

            category.IsActive = false; // 軟刪除
            await _context.SaveChangesAsync();
            return true;
        }

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
