using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addauditTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillDetails_PurchasesBillHeaders_PurchasesBillHeaderId",
                table: "PurchasesBillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_ClientVendors_ClientVendorId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_Companies_CompanyId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_UserProfiles_CreatedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeaders_UserProfiles_ModifiedByProfileId",
                table: "PurchasesBillHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasesBillHeaders",
                table: "PurchasesBillHeaders");

            migrationBuilder.RenameTable(
                name: "PurchasesBillHeaders",
                newName: "PurchasesBillHeader");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeaders_ModifiedByProfileId",
                table: "PurchasesBillHeader",
                newName: "IX_PurchasesBillHeader_ModifiedByProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeaders_CreatedByProfileId",
                table: "PurchasesBillHeader",
                newName: "IX_PurchasesBillHeader_CreatedByProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeaders_CompanyId",
                table: "PurchasesBillHeader",
                newName: "IX_PurchasesBillHeader_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeaders_ClientVendorId",
                table: "PurchasesBillHeader",
                newName: "IX_PurchasesBillHeader_ClientVendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasesBillHeader",
                table: "PurchasesBillHeader",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuditEntry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntry", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillDetails_PurchasesBillHeader_PurchasesBillHeaderId",
                table: "PurchasesBillDetails",
                column: "PurchasesBillHeaderId",
                principalTable: "PurchasesBillHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeader_ClientVendors_ClientVendorId",
                table: "PurchasesBillHeader",
                column: "ClientVendorId",
                principalTable: "ClientVendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeader_Companies_CompanyId",
                table: "PurchasesBillHeader",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeader_UserProfiles_CreatedByProfileId",
                table: "PurchasesBillHeader",
                column: "CreatedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeader_UserProfiles_ModifiedByProfileId",
                table: "PurchasesBillHeader",
                column: "ModifiedByProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillDetails_PurchasesBillHeader_PurchasesBillHeaderId",
                table: "PurchasesBillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeader_ClientVendors_ClientVendorId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeader_Companies_CompanyId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeader_UserProfiles_CreatedByProfileId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesBillHeader_UserProfiles_ModifiedByProfileId",
                table: "PurchasesBillHeader");

            migrationBuilder.DropTable(
                name: "AuditEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasesBillHeader",
                table: "PurchasesBillHeader");

            migrationBuilder.RenameTable(
                name: "PurchasesBillHeader",
                newName: "PurchasesBillHeaders");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeader_ModifiedByProfileId",
                table: "PurchasesBillHeaders",
                newName: "IX_PurchasesBillHeaders_ModifiedByProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeader_CreatedByProfileId",
                table: "PurchasesBillHeaders",
                newName: "IX_PurchasesBillHeaders_CreatedByProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeader_CompanyId",
                table: "PurchasesBillHeaders",
                newName: "IX_PurchasesBillHeaders_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesBillHeader_ClientVendorId",
                table: "PurchasesBillHeaders",
                newName: "IX_PurchasesBillHeaders_ClientVendorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasesBillHeaders",
                table: "PurchasesBillHeaders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillDetails_PurchasesBillHeaders_PurchasesBillHeaderId",
                table: "PurchasesBillDetails",
                column: "PurchasesBillHeaderId",
                principalTable: "PurchasesBillHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeaders_ClientVendors_ClientVendorId",
                table: "PurchasesBillHeaders",
                column: "ClientVendorId",
                principalTable: "ClientVendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesBillHeaders_Companies_CompanyId",
                table: "PurchasesBillHeaders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

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
        }
    }
}
