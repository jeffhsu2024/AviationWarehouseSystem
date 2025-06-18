using DataAccess.Data;
using DataAccess.IService;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DutyFreeProductService : Service<DutyFreeProduct>, IDutyFreeProductService
    {
        private readonly WarehouseContext _context;

        public DutyFreeProductService(WarehouseContext context) : base(context)
        {
            _context = context;
        }

        // 取得所有啟用商品，含分類與供應商
        public async Task<IEnumerable<DutyFreeProduct>> GetAllProductsAsync()
        {
            return await _context.DutyFreeProducts
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
        }

        // 取得指定商品（含分類與供應商）
        public async Task<DutyFreeProduct> GetProductByIdAsync(int id)
        {
            return await _context.DutyFreeProducts
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // 建立商品（可直接呼叫基底 CreateAsync，但如需額外處理可覆寫）
        public async Task<DutyFreeProduct> CreateProductAsync(DutyFreeProduct product)
        {
            await _context.DutyFreeProducts.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // 更新商品（可直接呼叫基底 UpdateAsync，但如需額外處理可覆寫）
        public async Task<DutyFreeProduct> UpdateProductAsync(DutyFreeProduct product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        // 軟刪除
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.DutyFreeProducts.FindAsync(id);
            if (product == null) return false;

            product.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // 搜尋商品
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

        // 庫存異動
        public async Task<bool> UpdateStockAsync(int productId, int quantity, TransactionType transactionType)
        {
            var product = await _context.DutyFreeProducts.FindAsync(productId);
            if (product == null) return false;

            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                TransactionType = transactionType,
                Quantity = quantity,
                TransactionDate = DateTime.Now,
                Remarks = $"{transactionType} - {quantity} units"
            };

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