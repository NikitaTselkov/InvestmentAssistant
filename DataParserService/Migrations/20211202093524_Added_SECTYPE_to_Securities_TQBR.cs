using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class Added_SECTYPE_to_Securities_TQBR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Securities_TQBR");

            migrationBuilder.CreateTable(
                name: "Securitie_TQBR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SECID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SECNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SECTYPE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securitie_TQBR", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Securitie_TQBR");

            migrationBuilder.CreateTable(
                name: "Securities_TQBR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SECID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SECNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities_TQBR", x => x.Id);
                });
        }
    }
}
