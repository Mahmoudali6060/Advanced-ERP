using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateSalesAndPurchaseHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Remaining",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "PurchasesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Remaining",
                table: "PurchasesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ClientVendors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientVendors_FullName",
                table: "ClientVendors",
                column: "FullName",
                unique: true,
                filter: "[FullName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientVendors_FullName",
                table: "ClientVendors");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "PurchasesBillHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ClientVendors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
