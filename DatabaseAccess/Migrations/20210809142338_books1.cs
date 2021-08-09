using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class books1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizCompletionHistory_QuizCompletionHistoryId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Letter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Letter_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Letter_PageId",
                table: "Letter",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_BookId",
                table: "Page",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizCompletionHistory_QuizCompletionHistoryId",
                table: "QuizQuestionUserAnswer",
                column: "QuizCompletionHistoryId",
                principalTable: "QuizCompletionHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                column: "QuizQuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizCompletionHistory_QuizCompletionHistoryId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.DropTable(
                name: "Letter");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizCompletionHistory_QuizCompletionHistoryId",
                table: "QuizQuestionUserAnswer",
                column: "QuizCompletionHistoryId",
                principalTable: "QuizCompletionHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                column: "QuizQuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "Id");
        }
    }
}
