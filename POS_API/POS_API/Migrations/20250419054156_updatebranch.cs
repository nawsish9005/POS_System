using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_API.Migrations
{
    /// <inheritdoc />
    public partial class updatebranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Branches_BranchesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BranchesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BranchesId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BranchId",
                table: "Products",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Branches_BranchId",
                table: "Products",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Branches_BranchId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BranchId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BranchesId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BranchesId",
                table: "Products",
                column: "BranchesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Branches_BranchesId",
                table: "Products",
                column: "BranchesId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
