using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Data.Migrations
{
    public partial class Initaldatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Dob",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    RequirementUser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RequirementDateStart = table.Column<DateTime>(nullable: false),
                    RequirementDateEnd = table.Column<DateTime>(nullable: false),
                    RequirementNote = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FixedUser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    FixedDateStart = table.Column<DateTime>(nullable: false),
                    FixedDateEnd = table.Column<DateTime>(nullable: false),
                    FixedNote = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Result = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RoomId = table.Column<int>(nullable: false),
                    Image = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    BuyDate = table.Column<DateTime>(nullable: false),
                    MaintainDate = table.Column<DateTime>(nullable: false),
                    MaintainTimes = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
