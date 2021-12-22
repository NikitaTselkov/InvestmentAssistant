using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class Updated_Companies_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capitalization_Of_Companies");

            migrationBuilder.DropColumn(
                name: "SECNAME",
                table: "Securities_TQBR");

            migrationBuilder.DropColumn(
                name: "SHORTNAME",
                table: "Securities_TQBR");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Securities_TQBR",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePeriod",
                table: "Multiplicators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Securities_TQBR_CompanyId",
                table: "Securities_TQBR",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Securities_TQBR_Companies_CompanyId",
                table: "Securities_TQBR",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Securities_TQBR_Companies_CompanyId",
                table: "Securities_TQBR");

            migrationBuilder.DropIndex(
                name: "IX_Securities_TQBR_CompanyId",
                table: "Securities_TQBR");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Securities_TQBR");

            migrationBuilder.DropColumn(
                name: "TimePeriod",
                table: "Multiplicators");

            migrationBuilder.AddColumn<string>(
                name: "SECNAME",
                table: "Securities_TQBR",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SHORTNAME",
                table: "Securities_TQBR",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Capitalization_Of_Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAPITALIZATION = table.Column<double>(type: "float", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SECID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitalization_Of_Companies", x => x.Id);
                });
        }
    }
}
