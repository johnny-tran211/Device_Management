using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Migrations
{
    public partial class AddCartDBsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartDBs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDBs", x => new { x.Id, x.ProductName });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDBs");
        }
    }
}
