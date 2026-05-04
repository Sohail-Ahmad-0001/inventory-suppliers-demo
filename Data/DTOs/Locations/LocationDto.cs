using inventory_suppliers.Data.DTOs.Suppliers;
using inventory_suppliers.Enums;

namespace inventory_suppliers.Data.DTOs.Locations;

public class LocationDto
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public required string City { get; set; }
    
    public required string State { get; set; }

    public required string Country { get; set; }

    public required string PostalCode { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public StatusEnum Status { get; set; }

    public required SupplierDto Supplier { get; set; }
}
