using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddMetaDataToTrackingTabelv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ProductTrackings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserProfileId",
                table: "ProductTrackings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ProductTrackings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
