using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddProductTrackingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductTrackings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductProcessTypeId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByProfileId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedByProfileId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedByProfileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByProfileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTrackings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTrackings_ProductId",
                table: "ProductTrackings",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTrackings");
        }
    }
}
