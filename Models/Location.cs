using inventory_suppliers.Enums;

namespace inventory_suppliers.Models;

public class Location
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public required string City { get; set; }

    public string State { get; set; } = string.Empty;

    public required string Country { get; set; }

    public string PostalCode { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public StatusEnum Status { get; set; }

    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    public Supplier Supplier { get; set; }
}