using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcLearning.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixing_bucket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1116c743-3584-4d54-82bd-2550729c211d");

            migrationBuilder.AddColumn<Guid>(
                name: "BucketId1",
                table: "BucketProduct",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0137150-c8db-4a0c-8a11-8c53cb042ee4", null, "ShopOwner", "SHOPOWNER" });

            migrationBuilder.CreateIndex(
                name: "IX_BucketProduct_BucketId1",
                table: "BucketProduct",
                column: "BucketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProduct_Buckets_BucketId1",
                table: "BucketProduct",
                column: "BucketId1",
                principalTable: "Buckets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketProduct_Buckets_BucketId1",
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1116c743-3584-4d54-82bd-2550729c211d", null, "ShopOwner", "SHOPOWNER" });
        }
    }
}
