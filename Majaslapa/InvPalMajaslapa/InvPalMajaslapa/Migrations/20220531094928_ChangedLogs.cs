using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvPalMajaslapa.Migrations
{
    public partial class ChangedLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Items_ItemsId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_ItemsId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Logs");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemsId",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ItemsId",
                table: "Logs",
                column: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Items_ItemsId",
                table: "Logs",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
