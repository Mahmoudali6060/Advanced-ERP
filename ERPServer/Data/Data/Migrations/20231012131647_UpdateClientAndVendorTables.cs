using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateClientAndVendorTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Vendors",
                newName: "PhoneNumber2");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clients",
                newName: "PhoneNumber2");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber1",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber1",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber1",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber2",
                table: "Vendors",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber2",
                table: "Clients",
                newName: "Phone");
        }
    }
}
