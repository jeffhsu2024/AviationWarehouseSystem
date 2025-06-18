using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategoryAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);

        Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory productCategory);

        Task<bool> DeleteProductCategoryAsync(int id);

        Task<IEnumerable<ProductCategory>> SearchProductCategoryAsync(string searchTerm);


    }
}
