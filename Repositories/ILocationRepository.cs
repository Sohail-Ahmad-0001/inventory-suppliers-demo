using inventory_suppliers.Data.DTOs.Locations;
using inventory_suppliers.Enums;
using inventory_suppliers.Models;

namespace inventory_suppliers.Repositories.Interfaces;

public interface ILocationRepository
{
    Task<(List<Location> items, int total)> GetAllAsync(
        Guid supplierId,
        int pageNumber,
        int pageSize,
        string? name,
        string? city,
        string? state,
        string? country,
        StatusEnum? status);

    Task<Location?> GetByIdAsync(Guid supplierId, Guid id);

    Task<Location> CreateAsync(Location location);

    Task<Location?> UpdateAsync(Guid supplierId, Guid id, UpdateLocationDto dto);

    Task<Location?> DeleteAsync(Guid supplierId, Guid id);
}
