using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Settings");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Settings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PurchasesBillInstructions",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesBillInstructions",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_CompanyId",
                table: "Settings",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Companies_CompanyId",
                table: "Settings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Companies_CompanyId",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_CompanyId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PurchasesBillInstructions",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "SalesBillInstructions",
                table: "Settings");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Settings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
