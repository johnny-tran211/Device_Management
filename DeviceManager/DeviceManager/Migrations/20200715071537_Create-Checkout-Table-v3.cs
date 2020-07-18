using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Migrations
{
    public partial class CreateCheckoutTablev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BuyDate",
                table: "CheckOuts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyDate",
                table: "CheckOuts");
        }
    }
}
