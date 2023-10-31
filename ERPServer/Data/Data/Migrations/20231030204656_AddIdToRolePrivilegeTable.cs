using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddIdToRolePrivilegeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePrivileges",
                table: "RolePrivileges");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "RolePrivileges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RolePrivileges",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "RolePrivileges",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePrivileges",
                table: "RolePrivileges",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePrivileges",
                table: "RolePrivileges");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolePrivileges");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "RolePrivileges");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "RolePrivileges");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePrivileges",
                table: "RolePrivileges",
                column: "PrivilegeId");
        }
    }
}
