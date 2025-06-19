using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface ITaxableGoodsService : IService<TaxableGoods>
    {
        Task<IEnumerable<TaxableGoods>> SearchAsync(string searchTerm);
        Task<IEnumerable<TaxableGoods>> GetAllWithDetailsAsync();
        Task<TaxableGoods> GetByIdWithDetailsAsync(int id);
    }
}