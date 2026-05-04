using inventory_suppliers.Data.DTOs.Locations;
using inventory_suppliers.Data.DTOs.Suppliers;
using inventory_suppliers.Models;

namespace inventory_suppliers.Mappers;

public static class LocationMapper
{
    public static LocationDto ToDto(Location location, SupplierDto? supplierSummary = null)
    {
        SupplierDto supplierDto = ResolveSupplierProjection(location, supplierSummary);

        return new LocationDto
        {
            Id = location.Id,
            SupplierId = location.SupplierId,
            Name = location.Name,
            Address = location.Address,
            City = location.City,
            State = location.State,
            Country = location.Country,
            PostalCode = location.PostalCode,
            CreatedAt = location.CreatedAt,
            UpdatedAt = location.UpdatedAt,
            Status = location.Status,
            DeletedAt = location.DeletedAt,
            Supplier = supplierDto
        };
    }

    private static SupplierDto ResolveSupplierProjection(Location location, SupplierDto? supplierSummary)
    {
        if (supplierSummary is not null)
        {
            return supplierSummary;
        }

        if (location.Supplier is not null)
        {
            return SupplierMapper.ToSummaryDto(location.Supplier);
        }

        throw new InvalidOperationException(
            "Supplier navigation must be loaded, or an explicit supplier summary must be supplied for mapping.");
    }
}
