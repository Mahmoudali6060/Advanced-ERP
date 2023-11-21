using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddTwoColsToAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Changes",
                table: "AuditEntry",
                newName: "OldData");

            migrationBuilder.AddColumn<string>(
                name: "NewData",
                table: "AuditEntry",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewData",
                table: "AuditEntry");

            migrationBuilder.RenameColumn(
                name: "OldData",
                table: "AuditEntry",
                newName: "Changes");
        }
    }
}
