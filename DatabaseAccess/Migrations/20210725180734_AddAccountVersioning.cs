using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class AddAccountVersioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Accounts",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08b335ba-bd9c-4ed0-8fb0-23c4551f272d",
                columns: new[] { "NormalizedUserName", "SecurityStamp", "UserName" },
                values: new object[] { "ADMIN@ADMIN.COM", "e0fef372-3022-4681-8889-95571a6dddf3", "admin@admin.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08b335ba-bd9c-4ed0-8fb0-23c4551f272d",
                columns: new[] { "NormalizedUserName", "SecurityStamp", "UserName" },
                values: new object[] { "ADMIN", "11ab0586-5e41-4a60-ae7c-eaffe242d3e8", "admin" });
        }
    }
}
