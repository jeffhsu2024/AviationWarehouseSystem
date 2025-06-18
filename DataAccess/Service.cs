using DataAccess.Data;
using DataAccess.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly WarehouseContext _db;
        internal DbSet<T> dbSet;
        public Service(WarehouseContext context)
        {
            _db = context;
            dbSet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
                return false;

            dbSet.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> SearchAsync(string searchTerm)
        {
            // 預設搜尋：嘗試以 ToString() 比對
            return await dbSet
                .Where(e => e.ToString().Contains(searchTerm))
                .ToListAsync();
        }
    }
}