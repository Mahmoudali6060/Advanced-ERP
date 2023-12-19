using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateBillsV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TreasuryId",
                table: "PurchasesBillHeader",
                type: "bigint",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeader_Treasuries_TreasuryId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesBillHeader_TreasuryId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "TreasuryId",
                table: "PurchasesBillHeader");
        }
    }
}
