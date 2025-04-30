using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class ebook_ecom_orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "day_counts",
                table: "tbl_order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "product_price",
                table: "tbl_order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "product_quantity",
                table: "tbl_order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "tbl_order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "day_counts",
                table: "tbl_order");

            migrationBuilder.DropColumn(
                name: "product_price",
                table: "tbl_order");

            migrationBuilder.DropColumn(
                name: "product_quantity",
                table: "tbl_order");

            migrationBuilder.DropColumn(
                name: "status",
                table: "tbl_order");
        }
    }
}
