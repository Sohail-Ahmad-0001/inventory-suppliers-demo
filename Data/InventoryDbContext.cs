using Microsoft.EntityFrameworkCore;
using inventory_suppliers.Models;

namespace inventory_suppliers.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Supplier)
                .WithMany(s => s.Locations)
                .HasForeignKey(l => l.SupplierId);

            var seedTime = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc);

            var supplierAcme = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f");
            var supplierNorth = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c");
            var supplierCoast = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434");

            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = supplierAcme,
                    Name = "Acme Wholesale",
                    Email = "orders@acmewholesale.example",
                    Code = "ACME",
                    Phone = "+64 9 555 0100",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                },
                new Supplier
                {
                    Id = supplierNorth,
                    Name = "Northland Produce Co.",
                    Email = "dispatch@northlandproduce.example",
                    Code = "NLD",
                    Phone = "+64 9 555 0200",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                },
                new Supplier
                {
                    Id = supplierCoast,
                    Name = "Coastline Imports",
                    Email = "imports@coastline.example",
                    Code = "CST",
                    Phone = "+64 4 555 0300",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                }
            };

            var locations = new List<Location>
            {
                new Location
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    SupplierId = supplierAcme,
                    Name = "Acme Auckland DC",
                    Address = "42 Distribution Way",
                    City = "Auckland",
                    State = "Auckland",
                    Country = "New Zealand",
                    PostalCode = "1010",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                },
                new Location
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    SupplierId = supplierAcme,
                    Name = "Acme Wellington Hub",
                    Address = "18 Harbour Quay",
                    City = "Wellington",
                    State = "Wellington",
                    Country = "New Zealand",
                    PostalCode = "6011",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                },
                new Location
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    SupplierId = supplierNorth,
                    Name = "Northland Cold Store",
                    Address = "200 Orchard Road",
                    City = "Whangārei",
                    State = "Northland",
                    Country = "New Zealand",
                    PostalCode = "0110",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                },
                new Location
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    SupplierId = supplierCoast,
                    Name = "Coastline Christchurch Depot",
                    Address = "5 Freight Lane",
                    City = "Christchurch",
                    State = "Canterbury",
                    Country = "New Zealand",
                    PostalCode = "8011",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                },
                new Location
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    SupplierId = supplierCoast,
                    Name = "Coastline Dunedin Office",
                    Address = "9 Port Street",
                    City = "Dunedin",
                    State = "Otago",
                    Country = "New Zealand",
                    PostalCode = "9016",
                    CreatedAt = seedTime,
                    UpdatedAt = seedTime,
                    DeletedAt = null
                }
            };

            modelBuilder.Entity<Supplier>().HasData(suppliers);
            modelBuilder.Entity<Location>().HasData(locations);
        }
    }
}
