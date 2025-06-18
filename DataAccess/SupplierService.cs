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
    public class SupplierService : Service<Supplier>, ISupplierService
    {
        private readonly WarehouseContext _context;
        public SupplierService(WarehouseContext context) : base(context)
        {
            _context = context;
        }
    }
}
