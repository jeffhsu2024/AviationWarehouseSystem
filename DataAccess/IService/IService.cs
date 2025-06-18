using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<T>> SearchAsync(string searchTerm);
    }
}