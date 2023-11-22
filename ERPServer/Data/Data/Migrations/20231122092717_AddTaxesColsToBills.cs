using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddTaxesColsToBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTax",
                table: "SalesBillHeaders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentage",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAfterVAT",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatAmount",
                table: "SalesBillHeaders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsTax",
                table: "PurchasesBillHeader",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "PurchasesBillHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentage",
                table: "PurchasesBillHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAfterVAT",
                table: "PurchasesBillHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "PurchasesBillHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatAmount",
                table: "PurchasesBillHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTax",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "TotalAfterVAT",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "IsTax",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "TotalAfterVAT",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "PurchasesBillHeader");
        }
    }
}
