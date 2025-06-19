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
    public class TaxableGoodsService : Service<TaxableGoods>, ITaxableGoodsService
    {
        private readonly WarehouseContext _context;

        public TaxableGoodsService(WarehouseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxableGoods>> GetAllWithDetailsAsync()
        {
            return await _context.TaxableGoods
                .Include(g => g.TaxCategory)
                .Include(g => g.Supplier)
                .Include(g => g.StorageLocation)
                .OrderBy(g => g.GoodsName)
                .ToListAsync();
        }

        public async Task<TaxableGoods> GetByIdWithDetailsAsync(int id)
        {
            return await _context.TaxableGoods
                .Include(g => g.TaxCategory)
                .Include(g => g.Supplier)
                .Include(g => g.StorageLocation)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<TaxableGoods>> SearchAsync(string searchTerm)
        {
            return await _context.TaxableGoods
                .Include(g => g.TaxCategory)
                .Include(g => g.Supplier)
                .Include(g => g.StorageLocation)
                .Where(g => g.GoodsName.Contains(searchTerm) || 
                            g.GoodsCode.Contains(searchTerm) ||
                            g.EnglishName.Contains(searchTerm) ||
                            g.TariffCode.Contains(searchTerm))
                .OrderBy(g => g.GoodsName)
                .ToListAsync();
        }
    }
}