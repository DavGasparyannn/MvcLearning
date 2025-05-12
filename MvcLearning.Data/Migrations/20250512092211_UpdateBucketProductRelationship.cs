using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcLearning.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBucketProductRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketProduct_Products_ProductsId",
                table: "BucketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BucketProduct",
                table: "BucketProduct");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72564700-bc98-4337-8572-99d85402f05f");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "BucketProduct",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BucketProduct_ProductsId",
                table: "BucketProduct",
                newName: "IX_BucketProduct_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BucketProduct",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BucketProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BucketProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

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
                values: new object[] { "1116c743-3584-4d54-82bd-2550729c211d", null, "ShopOwner", "SHOPOWNER" });

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProduct_Products_ProductId",
                table: "BucketProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketProduct_Products_ProductId",
                table: "BucketProduct");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BucketProduct_BucketId_ProductId",
                table: "BucketProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BucketProduct",
                table: "BucketProduct");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1116c743-3584-4d54-82bd-2550729c211d");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BucketProduct");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BucketProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BucketProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "BucketProduct",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_BucketProduct_ProductId",
                table: "BucketProduct",
                newName: "IX_BucketProduct_ProductsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BucketProduct",
                table: "BucketProduct",
                columns: new[] { "BucketId", "ProductsId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "72564700-bc98-4337-8572-99d85402f05f", null, "ShopOwner", "SHOPOWNER" });

            migrationBuilder.AddForeignKey(
                name: "FK_BucketProduct_Products_ProductsId",
                table: "BucketProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
