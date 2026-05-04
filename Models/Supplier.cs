namespace inventory_suppliers.Models;

public class Supplier
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Code { get; set; }

    public string Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}