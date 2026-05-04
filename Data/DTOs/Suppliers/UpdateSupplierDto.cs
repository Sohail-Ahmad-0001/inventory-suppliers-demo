namespace inventory_suppliers.Data.DTOs.Suppliers;

public class UpdateSupplierDto
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Code { get; set; }

    public required string Phone { get; set; }
}
