using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoanBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BooksId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "Loans",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BooksId",
                table: "Loans",
                newName: "IX_Loans_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Loans",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                newName: "IX_Loans_BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BooksId",
                table: "Loans",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
