using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddNewTrialAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsBlocked", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 3L, 0, 3L, "598bfed1-7f02-4ba9-bf1d-2d4a6b5c9fda", null, false, false, true, null, null, "TRIAL@TOURMALINECORE.COM", "AQAAAAIAAYagAAAAEMjyazORDOYnqIl3xzIAWWw1SBxxuOZ9yuh84b/RLk46r3JlVzkeMjbXZGOmHSu9mw==", null, false, "90c670cf-30e0-4de8-84db-83fd5ad03e94", false, "trial@tourmalinecore.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
