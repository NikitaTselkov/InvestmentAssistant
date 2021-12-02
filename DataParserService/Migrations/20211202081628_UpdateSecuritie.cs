using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class UpdateSecuritie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Securities");

            migrationBuilder.CreateTable(
                name: "Securities_TQBR",
                columns: table => new
                {
                    SECID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SHORTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SECNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Securities_TQBR");

            migrationBuilder.CreateTable(
                name: "Securities",
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
                    table.PrimaryKey("PK_Securities", x => x.Id);
                });
        }
    }
}
