using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Migrations
{
    public partial class CreateCheckoutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckOuts",
                columns: table => new
                {
                    CheckOutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiperId = table.Column<Guid>(nullable: false),
                    CartId = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Subtotal = table.Column<double>(nullable: false),
                    EstimatedShipping = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.CheckOutId);
                });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckOuts");

        }
    }
}
