using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvPalMajaslapa.Migrations
{
    public partial class AddedUserIdToWorkers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_AspNetUsers_ApplicationUserId",
                table: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Worker",
                table: "Worker");

            migrationBuilder.RenameTable(
                name: "Worker",
                newName: "Workers");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_ApplicationUserId",
                table: "Workers",
                newName: "IX_Workers_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers",
                table: "Workers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AspNetUsers_ApplicationUserId",
                table: "Workers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AspNetUsers_ApplicationUserId",
                table: "Workers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workers");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "Worker");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_ApplicationUserId",
                table: "Worker",
                newName: "IX_Worker_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Worker",
                table: "Worker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_AspNetUsers_ApplicationUserId",
                table: "Worker",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
