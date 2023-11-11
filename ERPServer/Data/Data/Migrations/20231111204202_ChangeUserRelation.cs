using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangeUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_RoleGroups_RoleId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "SalesBillDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "SalesBillDetails");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "RolePrivileges");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "RolePrivileges");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "RoleGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "RoleGroups");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "PurchasesBillDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "PurchasesBillDetails");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "ClientVendors");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "ClientVendors");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "AboutUs");

            migrationBuilder.RenameColumn(
                name: "ModifiedProfileId",
                table: "SalesBillHeaders",
                newName: "ModifiedByProfileId");

            migrationBuilder.RenameColumn(
                name: "ModifiedProfileId",
                table: "PurchasesBillHeaders",
                newName: "ModifiedByProfileId");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                table: "UserProfiles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_SalesBillHeaders_CreatedByProfileId",
                table: "SalesBillHeaders",
                column: "CreatedByProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesBillHeaders_ModifiedByProfileId",
                table: "SalesBillHeaders",
                column: "ModifiedByProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesBillHeaders_CreatedByProfileId",
                table: "PurchasesBillHeaders",
                column: "CreatedByProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesBillHeaders_ModifiedByProfileId",
                table: "PurchasesBillHeaders",
                column: "ModifiedByProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeaders_UserProfiles_CreatedByProfileId",
                table: "PurchasesBillHeaders",
                column: "CreatedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeaders_UserProfiles_ModifiedByProfileId",
                table: "PurchasesBillHeaders",
                column: "ModifiedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesBillHeaders_UserProfiles_CreatedByProfileId",
                table: "SalesBillHeaders",
                column: "CreatedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesBillHeaders_UserProfiles_ModifiedByProfileId",
                table: "SalesBillHeaders",
                column: "ModifiedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_RoleGroups_RoleId",
                table: "UserProfiles",
                column: "RoleId",
                principalTable: "RoleGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_UserProfiles_CreatedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_UserProfiles_ModifiedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesBillHeaders_UserProfiles_CreatedByProfileId",
                table: "SalesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesBillHeaders_UserProfiles_ModifiedByProfileId",
                table: "SalesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_RoleGroups_RoleId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SalesBillHeaders_CreatedByProfileId",
                table: "SalesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SalesBillHeaders_ModifiedByProfileId",
                table: "SalesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesBillHeaders_CreatedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesBillHeaders_ModifiedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.RenameColumn(
                name: "ModifiedByProfileId",
                table: "SalesBillHeaders",
                newName: "ModifiedProfileId");

            migrationBuilder.RenameColumn(
                name: "ModifiedByProfileId",
                table: "PurchasesBillHeaders",
                newName: "ModifiedProfileId");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                table: "UserProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "UserProfiles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "UserProfiles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "SalesBillDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "SalesBillDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "RolePrivileges",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "RolePrivileges",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "RoleGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "RoleGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "PurchasesBillDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "PurchasesBillDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "ContactUs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "ContactUs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "ClientVendors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "ClientVendors",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "Advertisments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "Advertisments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "AboutUs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "AboutUs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_RoleGroups_RoleId",
                table: "UserProfiles",
                column: "RoleId",
                principalTable: "RoleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
