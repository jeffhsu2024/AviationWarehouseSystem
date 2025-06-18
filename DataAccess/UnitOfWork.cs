using DataAccess.Data;
using DataAccess.IService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly WarehouseContext _db;
        public IDutyFreeProductService DutyFreeProduct { get; private set; }
        public IProductCategoryService ProductCategory { get; private set; }

        public ISupplierService Supplier { get; private set; }

        public UnitOfWork(WarehouseContext db)
        {
            _db = db;
            DutyFreeProduct = new DutyFreeProductService(_db);
            ProductCategory = new ProductCategoryService(_db);
            Supplier = new SupplierService(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
