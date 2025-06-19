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
    public class StorageLocationService : Service<StorageLocation>, IStorageLocationService
    {
        private readonly WarehouseContext _context;

        public StorageLocationService(WarehouseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StorageLocation>> GetAllActiveLocationsAsync()
        {
            return await _context.StorageLocations
                .Where(l => l.IsActive)
                .OrderBy(l => l.LocationCode)
                .ToListAsync();
        }

        public async Task<StorageLocation> GetLocationByIdAsync(int id)
        {
            return await _context.StorageLocations
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<StorageLocation>> GetLocationsByTypeAsync(StorageType storageType)
        {
            return await _context.StorageLocations
                .Where(l => l.IsActive && l.StorageType == storageType)
                .OrderBy(l => l.LocationCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<StorageLocation>> GetLocationsByZoneAsync(WarehouseZone zone)
        {
            return await _context.StorageLocations
                .Where(l => l.IsActive && l.WarehouseZone == zone)
                .OrderBy(l => l.LocationCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<StorageLocation>> SearchLocationsAsync(string searchTerm)
        {
            return await _context.StorageLocations
                .Where(l => l.IsActive && 
                           (l.LocationCode.Contains(searchTerm) ||
                            l.LocationName.Contains(searchTerm) ||
                            l.Floor.Contains(searchTerm) ||
                            l.Rack.Contains(searchTerm)))
                .OrderBy(l => l.LocationCode)
                .ToListAsync();
        }
    }
}
