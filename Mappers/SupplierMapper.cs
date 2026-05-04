using inventory_suppliers.Data.DTOs.Locations;
using inventory_suppliers.Data.DTOs.Suppliers;
using inventory_suppliers.Models;

namespace inventory_suppliers.Mappers;

public static class SupplierMapper
{
    /// <summary>
    /// Maps core supplier fields (no nested locations). Safe to reuse when embedding supplier on location DTOs.
    /// </summary>
    public static SupplierDto ToSummaryDto(Supplier supplier)
    {
        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Email = supplier.Email,
            Code = supplier.Code,
            Phone = supplier.Phone,
            CreatedAt = supplier.CreatedAt,
            UpdatedAt = supplier.UpdatedAt,
            Status = supplier.Status,
            DeletedAt = supplier.DeletedAt,
            Locations = new List<LocationDto>()
        };
    }

    public static SupplierDto ToDto(Supplier supplier, bool includeLocations = false)
    {
        SupplierDto dto = ToSummaryDto(supplier);

        if (!includeLocations || supplier.Locations is not { Count: > 0 })
        {
            return dto;
        }

        SupplierDto supplierSnapshotForNestedLocations = ToSummaryDto(supplier);

        dto.Locations = supplier.Locations
            .Select(location => LocationMapper.ToDto(location, supplierSnapshotForNestedLocations))
            .ToList();

        return dto;
    }
}
