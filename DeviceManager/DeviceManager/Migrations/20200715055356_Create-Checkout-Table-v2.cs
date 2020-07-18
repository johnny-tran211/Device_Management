using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Migrations
{
    public partial class CreateCheckoutTablev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CheckOuts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CheckOuts");
        }
    }
}
