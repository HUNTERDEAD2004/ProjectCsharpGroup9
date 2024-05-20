using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCsharpGroup9.Migrations
{
    /// <inheritdoc />
    public partial class LiemKhiet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartsDetails_Carts_CartDetailID",
                table: "CartsDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CartsDetails_CartID",
                table: "CartsDetails",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartsDetails_Carts_CartID",
                table: "CartsDetails",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartsDetails_Carts_CartID",
                table: "CartsDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartsDetails_CartID",
                table: "CartsDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CartsDetails_Carts_CartDetailID",
                table: "CartsDetails",
                column: "CartDetailID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
