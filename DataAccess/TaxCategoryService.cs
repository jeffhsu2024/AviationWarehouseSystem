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
    public class TaxCategoryService : Service<TaxCategory>, ITaxCategoryService
    {
        private readonly WarehouseContext _context;

        public TaxCategoryService(WarehouseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxCategory>> GetAllActiveCategoriesAsync()
        {
            return await _context.TaxCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ThenBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<TaxCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.TaxCategories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<TaxCategory>> GetSubCategoriesAsync(int parentId)
        {
            return await _context.TaxCategories
                .Where(c => c.ParentCategoryId == parentId && c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ThenBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaxCategory>> SearchCategoriesAsync(string searchTerm)
        {
            return await _context.TaxCategories
                .Where(c => c.IsActive && 
                           (c.CategoryName.Contains(searchTerm) ||
                            c.EnglishName.Contains(searchTerm) ||
                            c.CategoryCode.Contains(searchTerm) ||
                            c.TariffCode.Contains(searchTerm)))
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }
    }
}
