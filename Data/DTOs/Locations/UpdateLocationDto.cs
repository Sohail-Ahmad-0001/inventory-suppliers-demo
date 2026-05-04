using System.ComponentModel.DataAnnotations;

namespace inventory_suppliers.Data.DTOs.Locations;

public class UpdateLocationDto
{
    [Required]
    [MaxLength(150)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public required string Address { get; set; }

    [Required]
    [MaxLength(100)]
    public required string City { get; set; }

    [MaxLength(100)]
    public string? State { get; set; }

    [MaxLength(30)]
    public string? PostalCode { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Country { get; set; }
}
