using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IService
{
    public interface IStorageLocationService : IService<StorageLocation>
    {
        Task<IEnumerable<StorageLocation>> GetAllActiveLocationsAsync();
        Task<StorageLocation> GetLocationByIdAsync(int id);
        Task<IEnumerable<StorageLocation>> GetLocationsByTypeAsync(StorageType storageType);
        Task<IEnumerable<StorageLocation>> GetLocationsByZoneAsync(WarehouseZone zone);
        Task<IEnumerable<StorageLocation>> SearchLocationsAsync(string searchTerm);
    }
}
