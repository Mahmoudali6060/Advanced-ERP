using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UdpdateClientVendorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "ClientVendors",
                newName: "Debit");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "ClientVendors",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "ClientVendors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "ClientVendors");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "ClientVendors",
                newName: "Balance");

            migrationBuilder.AlterColumn<byte>(
                name: "TypeId",
                table: "ClientVendors",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
