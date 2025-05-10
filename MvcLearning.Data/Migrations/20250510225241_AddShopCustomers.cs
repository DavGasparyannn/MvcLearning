using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcLearning.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShopCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Shops_ShopId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShopId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ShopCustomers",
                columns: table => new
                {
                    CustomersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCustomers", x => new { x.CustomersId, x.ShopId });
                    table.ForeignKey(
                        name: "FK_ShopCustomers_AspNetUsers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopCustomers_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopCustomers_ShopId",
                table: "ShopCustomers",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopCustomers");

            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShopId",
                table: "AspNetUsers",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Shops_ShopId",
                table: "AspNetUsers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
