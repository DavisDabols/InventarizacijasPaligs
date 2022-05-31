using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvPalMajaslapa.Migrations
{
    public partial class UpdatedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Workers_WorkerId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_WorkerId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Logs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_WorkerId",
                table: "Logs",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Workers_WorkerId",
                table: "Logs",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
