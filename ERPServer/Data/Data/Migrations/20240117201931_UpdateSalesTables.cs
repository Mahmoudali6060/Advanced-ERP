using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateSalesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TreasuryId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TreasuryId",
                table: "PurchasesBillHeader",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesBillHeaders_TreasuryId",
                table: "SalesBillHeaders",
                column: "TreasuryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesBillHeader_TreasuryId",
                table: "PurchasesBillHeader",
                column: "TreasuryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeader_Treasuries_TreasuryId",
                table: "PurchasesBillHeader",
                column: "TreasuryId",
                principalTable: "Treasuries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesBillHeaders_Treasuries_TreasuryId",
                table: "SalesBillHeaders",
                column: "TreasuryId",
                principalTable: "Treasuries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeader_Treasuries_TreasuryId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesBillHeaders_Treasuries_TreasuryId",
                table: "SalesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SalesBillHeaders_TreasuryId",
                table: "SalesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesBillHeader_TreasuryId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "TreasuryId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "TreasuryId",
                table: "PurchasesBillHeader");
        }
    }
}
