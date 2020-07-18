using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Migrations
{
    public partial class updatecarttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CartDBs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "CartDBs");
        }
    }
}
