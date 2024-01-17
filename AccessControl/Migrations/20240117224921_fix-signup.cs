using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessControl.Migrations
{
    public partial class fixsignup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Accounts",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Accounts",
                newName: "Firstname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Accounts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Accounts",
                newName: "Nickname");
        }
    }
}
