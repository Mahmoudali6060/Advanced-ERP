using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RenameTransferColV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Transfer",
                table: "SalesBillHeaders",
                newName: "OtherExpenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherExpenses",
                table: "SalesBillHeaders",
                newName: "Transfer");
        }
    }
}
