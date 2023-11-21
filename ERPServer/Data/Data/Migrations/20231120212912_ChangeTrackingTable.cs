using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangeTrackingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileName",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "ModifiedByProfileId",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "ModifiedByProfileName",
                table: "ProductTrackings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductTrackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductTrackings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "ProductTrackings",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductTrackings");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "ProductTrackings");

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "ProductTrackings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByProfileName",
                table: "ProductTrackings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedByProfileId",
                table: "ProductTrackings",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByProfileName",
                table: "ProductTrackings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
