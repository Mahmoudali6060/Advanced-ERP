using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateSalesAndPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "SalesBillDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "PurchasesBillHeader",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "PurchasesBillDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "SalesBillDetails");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "PurchasesBillDetails");
        }
    }
}
