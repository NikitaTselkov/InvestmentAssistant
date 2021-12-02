using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class Added_Capitalization_Of_Companies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Securitie_TQBR",
                table: "Securitie_TQBR");

            migrationBuilder.RenameTable(
                name: "Securitie_TQBR",
                newName: "Securities_TQBR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Securities_TQBR",
                table: "Securities_TQBR",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Capitalization_Of_Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SECID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAPITALIZATION = table.Column<double>(type: "float", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitalization_Of_Companies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capitalization_Of_Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Securities_TQBR",
                table: "Securities_TQBR");

            migrationBuilder.RenameTable(
                name: "Securities_TQBR",
                newName: "Securitie_TQBR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Securitie_TQBR",
                table: "Securitie_TQBR",
                column: "Id");
        }
    }
}
