using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "UserProfiles",
                newName: "RoleName");

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "UserProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "UserProfiles",
                newName: "Role");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "UserProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
