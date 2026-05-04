namespace inventory_suppliers.Models;


public class Location
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    // Navigation properties
    public Supplier Supplier { get; set; }
}