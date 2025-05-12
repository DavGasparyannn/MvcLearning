using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcLearning.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketProduct_Buckets_BucketId",
                table: "BucketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BucketProduct_Buckets_BucketId1",
                table: "BucketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BucketProduct_Products_ProductId",
                table: "BucketProduct");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BucketProduct_BucketId_ProductId",
                table: "BucketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BucketProduct",
                table: "BucketProduct");

            migrationBuilder.DropIndex(
                name: "IX_BucketProduct_BucketId1",
                table: "BucketProduct");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0137150-c8db-4a0c-8a11-8c53cb042ee4");

            migrationBuilder.DropColumn(
                name: "BucketId1",
                table: "BucketProduct");

            migrationBuilder.RenameTable(
                name: "BucketProduct",
                newName: "BucketProducts");

            migrationBuilder.RenameIndex(
                name: "IX_BucketProduct_ProductId",
                table: "BucketProducts",
                newName: "IX_BucketProducts_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Buckets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BucketProducts_BucketId_ProductId",
                table: "BucketProducts",
                columns: new[] { "BucketId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BucketProducts",
                table: "BucketProducts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16db77ed-a8bd-4299-902f-be06c51137f1", null, "ShopOwner", "SHOPOWNER" });

            migrationBuilder.CreateIndex(
                name: "IX_Buckets_ProductId",
                table: "Buckets",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProducts_Buckets_BucketId",
                table: "BucketProducts",
                column: "BucketId",
                principalTable: "Buckets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProducts_Products_ProductId",
                table: "BucketProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_Products_ProductId",
                table: "Buckets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketProducts_Buckets_BucketId",
                table: "BucketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BucketProducts_Products_ProductId",
                table: "BucketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_Products_ProductId",
                table: "Buckets");

            migrationBuilder.DropIndex(
                name: "IX_Buckets_ProductId",
                table: "Buckets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BucketProducts_BucketId_ProductId",
                table: "BucketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BucketProducts",
                table: "BucketProducts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16db77ed-a8bd-4299-902f-be06c51137f1");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Buckets");

            migrationBuilder.RenameTable(
                name: "BucketProducts",
                newName: "BucketProduct");

            migrationBuilder.RenameIndex(
                name: "IX_BucketProducts_ProductId",
                table: "BucketProduct",
                newName: "IX_BucketProduct_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "BucketId1",
                table: "BucketProduct",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BucketProduct_BucketId_ProductId",
                table: "BucketProduct",
                columns: new[] { "BucketId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BucketProduct",
                table: "BucketProduct",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0137150-c8db-4a0c-8a11-8c53cb042ee4", null, "ShopOwner", "SHOPOWNER" });

            migrationBuilder.CreateIndex(
                name: "IX_BucketProduct_BucketId1",
                table: "BucketProduct",
                column: "BucketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProduct_Buckets_BucketId",
                table: "BucketProduct",
                column: "BucketId",
                principalTable: "Buckets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProduct_Buckets_BucketId1",
                table: "BucketProduct",
                column: "BucketId1",
                principalTable: "Buckets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProduct_Products_ProductId",
                table: "BucketProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
