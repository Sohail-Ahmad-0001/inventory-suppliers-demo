namespace inventory_suppliers.Data.DTOs.Suppliers;

public class CreateSupplierDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
}
