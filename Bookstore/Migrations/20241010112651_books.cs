using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_books",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    book_cover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qunatiy = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    auth_id = table.Column<int>(type: "int", nullable: false),
                    genres_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_books", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_books_tbl_authors_auth_id",
                        column: x => x.auth_id,
                        principalTable: "tbl_authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_books_tbl_genres_genres_id",
                        column: x => x.genres_id,
                        principalTable: "tbl_genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_books_auth_id",
                table: "tbl_books",
                column: "auth_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_books_genres_id",
                table: "tbl_books",
                column: "genres_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_books");
        }
    }
}
