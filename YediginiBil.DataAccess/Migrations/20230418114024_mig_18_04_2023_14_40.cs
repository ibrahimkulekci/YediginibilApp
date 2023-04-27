using Microsoft.EntityFrameworkCore.Migrations;

namespace YediginiBil.DataAccess.Migrations
{
    public partial class mig_18_04_2023_14_40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductBadges_BadgeId",
                table: "ProductBadges",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBadges_ProductId",
                table: "ProductBadges",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBadges_Badges_BadgeId",
                table: "ProductBadges",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBadges_Products_ProductId",
                table: "ProductBadges",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBadges_Badges_BadgeId",
                table: "ProductBadges");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBadges_Products_ProductId",
                table: "ProductBadges");

            migrationBuilder.DropIndex(
                name: "IX_ProductBadges_BadgeId",
                table: "ProductBadges");

            migrationBuilder.DropIndex(
                name: "IX_ProductBadges_ProductId",
                table: "ProductBadges");
        }
    }
}
