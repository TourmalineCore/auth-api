using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddDefaultCeo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 50L, 0, 1L, "15cfcf9e-94f3-4b65-884c-e03f1588fa20", null, false, false, true, null, null, "CEO@TOURMALINECORE.COM", "AQAAAAEAACcQAAAAEEim42ocT0sL9H818pD8yMxUDtA3GUCe+SDwlxl0geUX56G2GEvu7G6DKHjXMHowlA==", null, false, "LCZLJS5K7YP2FO2NFI2P6CQZVJLWEYZZ", false, "ceo@tourmalinecore.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 50L);
        }
    }
}