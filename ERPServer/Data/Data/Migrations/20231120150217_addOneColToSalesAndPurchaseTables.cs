using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addOneColToSalesAndPurchaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RepresentiveId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RepresentiveId",
                table: "PurchasesBillHeader",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepresentiveId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "RepresentiveId",
                table: "PurchasesBillHeader");
        }
    }
}
