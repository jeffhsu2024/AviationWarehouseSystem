using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IDutyFreeProductService
    {
        Task<IEnumerable<DutyFreeProduct>> GetAllProductsAsync();
        Task<DutyFreeProduct> GetProductByIdAsync(int id);
        Task<DutyFreeProduct> UpdateProductAsync(DutyFreeProduct product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<DutyFreeProduct>> SearchProductsAsync(string searchTerm);
        Task<bool> UpdateStockAsync(int productId, int quantity, TransactionType transactionType);
        Task<DutyFreeProduct> CreateProductAsync(DutyFreeProduct product);
    }
}
