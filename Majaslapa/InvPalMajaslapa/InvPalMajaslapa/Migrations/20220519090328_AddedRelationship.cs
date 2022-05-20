using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvPalMajaslapa.Migrations
{
    public partial class AddedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ApplicationUserId",
                table: "Items",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WarehouseId",
                table: "Items",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_ApplicationUserId",
                table: "Items",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Warehouses_WarehouseId",
                table: "Items",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_ApplicationUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Warehouses_WarehouseId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ApplicationUserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_WarehouseId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Items");
        }
    }
}
