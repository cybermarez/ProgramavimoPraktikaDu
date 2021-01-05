using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class HAHAHAASGRYBAs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "PurchaseHistorys");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PurchaseHistorys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "PurchaseHistorys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "PurchaseHistorys",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PurchaseHistorys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistorys_ProductId",
                table: "PurchaseHistorys",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistorys_UserId",
                table: "PurchaseHistorys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistorys_Products_ProductId",
                table: "PurchaseHistorys",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistorys_Users_UserId",
                table: "PurchaseHistorys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistorys_Products_ProductId",
                table: "PurchaseHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistorys_Users_UserId",
                table: "PurchaseHistorys");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistorys_ProductId",
                table: "PurchaseHistorys");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistorys_UserId",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "PurchaseHistorys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PurchaseHistorys");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PurchaseHistorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PurchaseHistorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "PurchaseHistorys",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "PurchaseHistorys",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
