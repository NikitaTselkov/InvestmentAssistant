using Microsoft.EntityFrameworkCore.Migrations;

namespace DataParserService.Migrations
{
    public partial class Update_Tabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "SecuritieTQBRId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SecuritieTQBRId",
                table: "Companies",
                column: "SecuritieTQBRId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Securities_TQBR_SecuritieTQBRId",
                table: "Companies",
                column: "SecuritieTQBRId",
                principalTable: "Securities_TQBR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Securities_TQBR_SecuritieTQBRId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SecuritieTQBRId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SecuritieTQBRId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Securities_TQBR",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
