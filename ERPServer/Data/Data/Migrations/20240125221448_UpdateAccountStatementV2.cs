using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateAccountStatementV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "AccountStatements");

            migrationBuilder.AddColumn<long>(
                name: "TreasuryId",
                table: "AccountStatements",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatements_TreasuryId",
                table: "AccountStatements",
                column: "TreasuryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountStatements_Treasuries_TreasuryId",
                table: "AccountStatements",
                column: "TreasuryId",
                principalTable: "Treasuries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountStatements_Treasuries_TreasuryId",
                table: "AccountStatements");

            migrationBuilder.DropIndex(
                name: "IX_AccountStatements_TreasuryId",
                table: "AccountStatements");

            migrationBuilder.DropColumn(
                name: "TreasuryId",
                table: "AccountStatements");

            migrationBuilder.AddColumn<int>(
                name: "AccountTypeId",
                table: "AccountStatements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
