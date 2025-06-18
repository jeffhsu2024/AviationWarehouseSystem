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
    public class DutyFreeProductService : IDutyFreeProductService
    {
        private readonly WarehouseContext _context;
        public DutyFreeProductService(WarehouseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DutyFreeProduct>> GetAllProductsAsync()
        {
            return await _context.DutyFreeProducts
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }
        public async Task<DutyFreeProduct> GetProductByIdAsync(int id)
        {
            return await _context.DutyFreeProducts
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<DutyFreeProduct> CreateProductAsync(DutyFreeProduct product)
        {
            _context.DutyFreeProducts.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<DutyFreeProduct> UpdateProductAsync(DutyFreeProduct product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.DutyFreeProducts.FindAsync(id);
            if (product == null) return false;

            product.IsActive = false; // 軟刪除
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DutyFreeProduct>> SearchProductsAsync(string searchTerm)
        {
            return await _context.DutyFreeProducts
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive &&
                       (p.ProductName.Contains(searchTerm) ||
                        p.ProductCode.Contains(searchTerm) ||
                        p.Brand.Contains(searchTerm)))
                .ToListAsync();
        }
        public async Task<bool> UpdateStockAsync(int productId, int quantity, TransactionType transactionType)
        {
            var product = await _context.DutyFreeProducts.FindAsync(productId);
            if (product == null) return false;

            // 建立庫存異動記錄
            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                TransactionType = transactionType,
                Quantity = quantity,
                TransactionDate = DateTime.Now,
                Remarks = $"{transactionType} - {quantity} units"
            };

            // 更新庫存數量
            if (transactionType == TransactionType.Purchase)
                product.StockQuantity += quantity;
            else if (transactionType == TransactionType.Sale)
                product.StockQuantity -= quantity;

            _context.InventoryTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
