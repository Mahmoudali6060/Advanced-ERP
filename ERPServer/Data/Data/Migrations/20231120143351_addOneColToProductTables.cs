using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addOneColToProductTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasurementId",
                table: "Products",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasurementId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
