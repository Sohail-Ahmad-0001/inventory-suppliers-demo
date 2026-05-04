namespace inventory_suppliers.Data.DTOs.Locations;

public class CreateLocationDto
{
    public required string Name { get; set; }

    public required string Address { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public required string Country { get; set; }

    public required string PostalCode { get; set; }
}
