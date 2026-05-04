using inventory_suppliers.Data.DTOs.Locations;
using inventory_suppliers.Enums;

namespace inventory_suppliers.Data.DTOs.Suppliers;

public class SupplierDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }
    
    public required string Phone { get; set; }

    public required string Code { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public StatusEnum Status { get; set; }
    
    public DateTime? DeletedAt { get; set; }

    public ICollection<LocationDto> Locations { get; set; } = new List<LocationDto>();
}
