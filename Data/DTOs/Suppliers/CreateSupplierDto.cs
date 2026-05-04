using System.ComponentModel.DataAnnotations;

namespace inventory_suppliers.Data.DTOs.Suppliers;

public class CreateSupplierDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Code { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 5)]
    [RegularExpression(@"^\+?[0-9][0-9\s\-()]{4,29}$", ErrorMessage = "Phone must be a valid number.")]
    public required string Phone { get; set; }
}
