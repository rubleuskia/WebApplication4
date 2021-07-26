using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class AddQuizFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionUserAnswer_QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                column: "QuizQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                column: "QuizQuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_QuizQuestionUserAnswer_QuizQuestionId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.DropColumn(
                name: "QuizQuestionId",
                table: "QuizQuestionUserAnswer");
        }
    }
}
