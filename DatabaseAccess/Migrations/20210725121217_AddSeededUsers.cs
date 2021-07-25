using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.Migrations
{
    public partial class AddSeededUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "266fa2f3-766c-42b4-a409-9a7abd6e0b84", "75645469-96e7-4aff-9119-019d84c16984", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "08b335ba-bd9c-4ed0-8fb0-23c4551f272d", 0, 100, "791eb30f-3fa2-4116-bc66-b67473be8e4c", "admin@admin.com", false, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEC27dnWY85PP6ENmRFIZpc04i3OusxPvrs/B9Jybhmt6hHy3KojwGcWq5D1KhCFutg==", null, false, "11ab0586-5e41-4a60-ae7c-eaffe242d3e8", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "266fa2f3-766c-42b4-a409-9a7abd6e0b84", "08b335ba-bd9c-4ed0-8fb0-23c4551f272d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "266fa2f3-766c-42b4-a409-9a7abd6e0b84", "08b335ba-bd9c-4ed0-8fb0-23c4551f272d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "266fa2f3-766c-42b4-a409-9a7abd6e0b84");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08b335ba-bd9c-4ed0-8fb0-23c4551f272d");
        }
    }
}
