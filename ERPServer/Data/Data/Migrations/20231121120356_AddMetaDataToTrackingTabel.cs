using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddMetaDataToTrackingTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUsername",
                table: "SalesBillHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUsername",
                table: "SalesBillHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUsername",
                table: "PurchasesBillHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUsername",
                table: "PurchasesBillHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "ProductTrackings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUsername",
                table: "ProductTrackings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedByProfileId",
                table: "ProductTrackings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUsername",
                table: "ProductTrackings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTrackings_CreatedByProfileId",
                table: "ProductTrackings",
                column: "CreatedByProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTrackings_ModifiedByProfileId",
                table: "ProductTrackings",
                column: "ModifiedByProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTrackings_UserProfiles_CreatedByProfileId",
                table: "ProductTrackings",
                column: "CreatedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTrackings_UserProfiles_ModifiedByProfileId",
                table: "ProductTrackings",
                column: "ModifiedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTrackings_UserProfiles_CreatedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTrackings_UserProfiles_ModifiedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropIndex(
                name: "IX_ProductTrackings_CreatedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropIndex(
                name: "IX_ProductTrackings_ModifiedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "CreatedByUsername",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedByUsername",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "CreatedByUsername",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "ModifiedByUsername",
                table: "PurchasesBillHeader");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "CreatedByUsername",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "ModifiedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "ModifiedByUsername",
                table: "ProductTrackings");
        }
    }
}
