using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addMetaDataInTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "CompanyId",
                table: "States",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "SalesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "SalesBillDetails",
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
                name: "CompanyId",
                table: "RoleGroups",
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
                name: "CompanyId",
                table: "PurchasesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "PurchasesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "PurchasesBillHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "PurchasesBillDetails",
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
                name: "CompanyId",
                table: "ContactUs",
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

            migrationBuilder.AddColumn<long>(
                name: "CreatedByProfileId",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedProfileId",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "ClientVendors",
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
                name: "CompanyId",
                table: "Cities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Categories",
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
                name: "CompanyId",
                table: "Advertisments",
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
                name: "CompanyId",
                table: "AboutUs",
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

            migrationBuilder.CreateIndex(
                name: "IX_States_CompanyId",
                table: "States",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesBillHeaders_CompanyId",
                table: "SalesBillHeaders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesBillDetails_CompanyId",
                table: "SalesBillDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroups_CompanyId",
                table: "RoleGroups",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesBillHeaders_CompanyId",
                table: "PurchasesBillHeaders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesBillDetails_CompanyId",
                table: "PurchasesBillDetails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_CompanyId",
                table: "ContactUs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientVendors_CompanyId",
                table: "ClientVendors",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisments_CompanyId",
                table: "Advertisments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutUs_CompanyId",
                table: "AboutUs",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUs_Companies_CompanyId",
                table: "AboutUs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_Companies_CompanyId",
                table: "Advertisments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Companies_CompanyId",
                table: "Categories",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Companies_CompanyId",
                table: "Cities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientVendors_Companies_CompanyId",
                table: "ClientVendors",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_Companies_CompanyId",
                table: "ContactUs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillDetails_Companies_CompanyId",
                table: "PurchasesBillDetails",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeaders_Companies_CompanyId",
                table: "PurchasesBillHeaders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGroups_Companies_CompanyId",
                table: "RoleGroups",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesBillDetails_Companies_CompanyId",
                table: "SalesBillDetails",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesBillHeaders_Companies_CompanyId",
                table: "SalesBillHeaders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Companies_CompanyId",
                table: "States",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUs_Companies_CompanyId",
                table: "AboutUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_Companies_CompanyId",
                table: "Advertisments");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Companies_CompanyId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Companies_CompanyId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientVendors_Companies_CompanyId",
                table: "ClientVendors");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_Companies_CompanyId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillDetails_Companies_CompanyId",
                table: "PurchasesBillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_Companies_CompanyId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleGroups_Companies_CompanyId",
                table: "RoleGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesBillDetails_Companies_CompanyId",
                table: "SalesBillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesBillHeaders_Companies_CompanyId",
                table: "SalesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Companies_CompanyId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_CompanyId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_SalesBillHeaders_CompanyId",
                table: "SalesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_SalesBillDetails_CompanyId",
                table: "SalesBillDetails");

            migrationBuilder.DropIndex(
                name: "IX_RoleGroups_CompanyId",
                table: "RoleGroups");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesBillHeaders_CompanyId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesBillDetails_CompanyId",
                table: "PurchasesBillDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_CompanyId",
                table: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ClientVendors_CompanyId",
                table: "ClientVendors");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Advertisments_CompanyId",
                table: "Advertisments");

            migrationBuilder.DropIndex(
                name: "IX_AboutUs_CompanyId",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "States");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "SalesBillHeaders");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "SalesBillDetails");

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
                name: "CompanyId",
                table: "RoleGroups");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "RoleGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "RoleGroups");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PurchasesBillDetails");

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
                name: "CompanyId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ClientVendors");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "ClientVendors");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "ClientVendors");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "CreatedByProfileId",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "ModifiedProfileId",
                table: "AboutUs");
        }
    }
}
