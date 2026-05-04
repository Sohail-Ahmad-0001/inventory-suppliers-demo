using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace inventory_suppliers.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Email", "Name", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), "ACME", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "orders@acmewholesale.example", "Acme Wholesale", "+64 9 555 0100", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), "NLD", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "dispatch@northlandproduce.example", "Northland Produce Co.", "+64 9 555 0200", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), "CST", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "imports@coastline.example", "Coastline Imports", "+64 4 555 0300", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "Country", "CreatedAt", "DeletedAt", "Name", "PostalCode", "State", "SupplierId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), "200 Orchard Road", "Whangārei", "New Zealand", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Northland Cold Store", "0110", "Northland", new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), "18 Harbour Quay", "Wellington", "New Zealand", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Acme Wellington Hub", "6011", "Wellington", new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), "9 Port Street", "Dunedin", "New Zealand", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Coastline Dunedin Office", "9016", "Otago", new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "5 Freight Lane", "Christchurch", "New Zealand", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Coastline Christchurch Depot", "8011", "Canterbury", new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "42 Distribution Way", "Auckland", "New Zealand", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Acme Auckland DC", "1010", "Auckland", new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_SupplierId",
                table: "Locations",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
