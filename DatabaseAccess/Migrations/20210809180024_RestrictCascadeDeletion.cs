using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class RestrictCascadeDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letter_Page_PageId",
                table: "Letter");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_Books_BookId",
                table: "Page");

            migrationBuilder.AddForeignKey(
                name: "FK_Letter_Page_PageId",
                table: "Letter",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_Books_BookId",
                table: "Page",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letter_Page_PageId",
                table: "Letter");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_Books_BookId",
                table: "Page");

            migrationBuilder.AddForeignKey(
                name: "FK_Letter_Page_PageId",
                table: "Letter",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_Books_BookId",
                table: "Page",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
