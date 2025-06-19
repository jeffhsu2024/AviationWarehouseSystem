using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface ITaxCategoryService : IService<TaxCategory>
    {
        Task<IEnumerable<TaxCategory>> GetAllActiveCategoriesAsync();
        Task<TaxCategory> GetCategoryByIdAsync(int id);
        Task<IEnumerable<TaxCategory>> GetSubCategoriesAsync(int parentId);
        Task<IEnumerable<TaxCategory>> SearchCategoriesAsync(string searchTerm);
    }
}
