using inventory_suppliers.Data.DTOs.Suppliers;
using inventory_suppliers.Enums;
using inventory_suppliers.Models;

namespace inventory_suppliers.Repositories.Interfaces;

public interface ISupplierRepository
{
    Task<(List<Supplier> items, int total)> GetAllAsync(
        int pageNumber,
        int pageSize,
        string? name,
        string? email,
        string? code,
        string? phone,
        bool includeLocations,
        StatusEnum? status);

    Task<Supplier?> GetByIdAsync(Guid id, bool includeLocations);

    Task<Supplier> CreateAsync(Supplier supplier);

    Task<Supplier?> UpdateAsync(Guid id, UpdateSupplierDto dto);

    Task<Supplier?> DeleteAsync(Guid id);
}
