using Microsoft.EntityFrameworkCore.Migrations;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    public partial class Added_CodeOfGroupOfMultiplicator_To_DescriptionsForMultiplicators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 1,
                column: "CodeOfGroupOfMultiplicator",
                value: "REVEM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 2,
                column: "CodeOfGroupOfMultiplicator",
                value: "REVEM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 3,
                column: "CodeOfGroupOfMultiplicator",
                value: "REVEM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 4,
                column: "CodeOfGroupOfMultiplicator",
                value: "BALANM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 5,
                column: "CodeOfGroupOfMultiplicator",
                value: "FSSM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 6,
                column: "CodeOfGroupOfMultiplicator",
                value: "REVEM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 7,
                column: "CodeOfGroupOfMultiplicator",
                value: "FSSM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 8,
                column: "CodeOfGroupOfMultiplicator",
                value: "PROFIM");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 9,
                column: "CodeOfGroupOfMultiplicator",
                value: "PROFIM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators");
        }
    }
}
