using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cvv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order", x => x.order_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_wishlist_product_id",
                table: "tbl_wishlist",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cart_product_id",
                table: "tbl_cart",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_cart_tbl_books_product_id",
                table: "tbl_cart",
                column: "product_id",
                principalTable: "tbl_books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_wishlist_tbl_books_product_id",
                table: "tbl_wishlist",
                column: "product_id",
                principalTable: "tbl_books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_cart_tbl_books_product_id",
                table: "tbl_cart");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_wishlist_tbl_books_product_id",
                table: "tbl_wishlist");

            migrationBuilder.DropTable(
                name: "tbl_order");

            migrationBuilder.DropIndex(
                name: "IX_tbl_wishlist_product_id",
                table: "tbl_wishlist");

            migrationBuilder.DropIndex(
                name: "IX_tbl_cart_product_id",
                table: "tbl_cart");
        }
    }
}
