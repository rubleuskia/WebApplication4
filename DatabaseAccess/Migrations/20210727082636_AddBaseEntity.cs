using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class AddBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastUpdatedById",
                table: "Accounts",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_LastUpdatedById",
                table: "Accounts",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_CreatedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_LastUpdatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LastUpdatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Accounts");
        }
    }
}
