using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testingmanagment.Migrations
{
    /// <inheritdoc />
    public partial class version1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseInBlock",
                columns: table => new
                {
                    Case = table.Column<Guid>(type: "uuid", nullable: false),
                    Block = table.Column<Guid>(type: "uuid", nullable: false),
                    Applicable = table.Column<bool>(type: "boolean", nullable: false),
                    Verdict = table.Column<int>(type: "integer", nullable: false),
                    TestReportComment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseInBlock", x => x.Case);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dut",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<Guid>(type: "uuid", nullable: false),
                    Company = table.Column<Guid>(type: "uuid", nullable: false),
                    FirmwareVersion = table.Column<int>(name: "Firmware_Version", type: "integer", nullable: false),
                    HardmwareVersion = table.Column<int>(name: "Hardmware_Version", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestBlock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TestProgram = table.Column<Guid>(type: "uuid", nullable: false),
                    TestGroup = table.Column<Guid>(type: "uuid", nullable: false),
                    TestEngineer = table.Column<Guid>(type: "uuid", nullable: false),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: false),
                    Recommendation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestBlock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProjectNumber = table.Column<int>(type: "integer", nullable: false),
                    Dut = table.Column<Guid>(type: "uuid", nullable: false),
                    TestInitiator = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestProgram", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseInBlock");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Dut");

            migrationBuilder.DropTable(
                name: "TestBlock");

            migrationBuilder.DropTable(
                name: "TestProgram");
        }
    }
}
