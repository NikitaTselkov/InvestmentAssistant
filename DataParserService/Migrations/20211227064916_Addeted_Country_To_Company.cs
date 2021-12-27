using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class Addeted_Country_To_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SectorShortName",
                table: "Companies",
                newName: "Sector");

            migrationBuilder.RenameColumn(
                name: "SectorLongName",
                table: "Companies",
                newName: "Industry");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Companies",
                newName: "SectorShortName");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "Companies",
                newName: "SectorLongName");
        }
    }
}
