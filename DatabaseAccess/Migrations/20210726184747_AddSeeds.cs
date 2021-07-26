using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class AddSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizCompletionHistory_AspNetUsers_UserId",
                table: "QuizCompletionHistory");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuizCompletionHistory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b9ef978-d85b-4050-b636-c0f4b4f4f708", "9906c2f4-4941-4f1e-ae6e-6b67258c526f", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bdb94046-f01d-4080-a989-341c3e88ed50", 0, 100, "c568026f-8944-41b6-8fcc-84c613158e27", "admin@admin.com", false, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEOlRAst5kneREHoKVJsYzh3MhWbA3Z9lJl2aw/Hk4DO9C9gtXT/CiI8Q7ND1arCpQA==", null, false, "7b8588f2-6429-4d36-9097-dcc81abdf4a7", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5b9ef978-d85b-4050-b636-c0f4b4f4f708", "bdb94046-f01d-4080-a989-341c3e88ed50" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizCompletionHistory_AspNetUsers_UserId",
                table: "QuizCompletionHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizCompletionHistory_AspNetUsers_UserId",
                table: "QuizCompletionHistory");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5b9ef978-d85b-4050-b636-c0f4b4f4f708", "bdb94046-f01d-4080-a989-341c3e88ed50" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b9ef978-d85b-4050-b636-c0f4b4f4f708");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bdb94046-f01d-4080-a989-341c3e88ed50");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QuizCompletionHistory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizCompletionHistory_AspNetUsers_UserId",
                table: "QuizCompletionHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
