using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateTreasuryV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Treasuries");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Treasuries",
                newName: "Debit");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "Treasuries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "TreasuryId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesBillHeaders_TreasuryId",
                table: "SalesBillHeaders",
                column: "TreasuryId");

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
                name: "FK_SalesBillHeaders_Treasuries_TreasuryId",
                table: "SalesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SalesBillHeaders_TreasuryId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Treasuries");

            migrationBuilder.DropColumn(
                name: "TreasuryId",
                table: "SalesBillHeaders");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "Treasuries",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "Treasuries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
