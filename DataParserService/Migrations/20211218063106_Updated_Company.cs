using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class Updated_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Companies",
                newName: "SectorShortName");

            migrationBuilder.AddColumn<string>(
                name: "SectorLongName",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectorLongName",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "SectorShortName",
                table: "Companies",
                newName: "Sector");
        }
    }
}
