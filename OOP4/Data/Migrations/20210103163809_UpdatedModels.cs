using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WishLists_WishListId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WishListId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishLists");

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WishListId",
                table: "Products",
                column: "WishListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WishLists_WishListId",
                table: "Products",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
