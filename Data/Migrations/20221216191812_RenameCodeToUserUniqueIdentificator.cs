using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RenameCodeToUserUniqueIdentificator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "AspNetUsers",
                newName: "UserUniqueIdentificator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserUniqueIdentificator",
                table: "AspNetUsers",
                newName: "Code");
        }
    }
}
