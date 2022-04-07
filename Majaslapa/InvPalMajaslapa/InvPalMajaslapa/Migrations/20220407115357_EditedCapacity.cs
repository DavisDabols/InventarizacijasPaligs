using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvPalMajaslapa.Migrations
{
    public partial class EditedCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCapacity",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "MaxCapacity",
                table: "Warehouses",
                type: "int",
                nullable: true);
        }
    }
}
