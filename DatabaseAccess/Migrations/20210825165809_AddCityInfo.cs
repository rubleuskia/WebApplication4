using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class AddCityInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.CreateTable(
                name: "CityInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherInfo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                column: "QuizQuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer");

            migrationBuilder.DropTable(
                name: "CityInfos");

            migrationBuilder.DropTable(
                name: "WeatherInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionUserAnswer_QuizQuestion_QuizQuestionId",
                table: "QuizQuestionUserAnswer",
                column: "QuizQuestionId",
                principalTable: "QuizQuestion",
                principalColumn: "Id");
        }
    }
}
