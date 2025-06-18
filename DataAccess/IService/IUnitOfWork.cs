using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IUnitOfWork
    {
        IDutyFreeProductService DutyFreeProduct { get; }
        IProductCategoryService ProductCategory { get; }

        ISupplierService Supplier { get; }
        void Save();
    }
}
