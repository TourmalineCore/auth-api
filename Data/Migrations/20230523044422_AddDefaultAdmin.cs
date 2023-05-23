using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddDefaultAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1L, 0, 1L, "15cfcf9e-94f3-4b65-884c-e03f1588fa20", null, false, false, true, null, null, "CEO@TOURMALINECORE.COM", "AQAAAAEAACcQAAAAEEim42ocT0sL9H818pD8yMxUDtA3GUCe+SDwlxl0geUX56G2GEvu7G6DKHjXMHowlA==", null, false, "LCZLJS5K7YP2FO2NFI2P6CQZVJLWEYZZ", false, "ceo@tourmalinecore.com" },
                    { 2L, 0, 2L, "6ef5ca6b-8ff7-4431-84f1-c04b1b4198b3", null, false, false, true, null, null, "INNER-CIRCLE-ADMIN@TOURMALINECORE.COM", "AQAAAAEAACcQAAAAEHeZpxgabtPpnFcKHWwBDedWSCuW7l6V0Kz/RmxCayiauzadB9IZHoqilHnSOPhrFQ==", null, false, "BSBX3FSL64CHWY2LDLEJLXO27HC43KX4", false, "inner-circle-admin@tourmalinecore.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 50L, 0, 1L, "15cfcf9e-94f3-4b65-884c-e03f1588fa20", null, false, false, true, null, null, "CEO@TOURMALINECORE.COM", "AQAAAAEAACcQAAAAEEim42ocT0sL9H818pD8yMxUDtA3GUCe+SDwlxl0geUX56G2GEvu7G6DKHjXMHowlA==", null, false, "LCZLJS5K7YP2FO2NFI2P6CQZVJLWEYZZ", false, "ceo@tourmalinecore.com" });
        }
    }
}
