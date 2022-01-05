using Microsoft.EntityFrameworkCore.Migrations;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    public partial class Added_HowToInterpret_To_DescriptionForMultiplicators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IfNeedToConsiderTheDynamics",
                table: "Multiplicators");

            migrationBuilder.AddColumn<string>(
                name: "HowToInterpret",
                table: "DescriptionsForMultiplicators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 1,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 2,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 3,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 4,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 5,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 6,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 7,
                column: "HowToInterpret",
                value: "Чем меньше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 8,
                column: "HowToInterpret",
                value: "Чем больше, тем лучше");

            migrationBuilder.UpdateData(
                table: "DescriptionsForMultiplicators",
                keyColumn: "Id",
                keyValue: 9,
                column: "HowToInterpret",
                value: "Чем больше, тем лучше");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HowToInterpret",
                table: "DescriptionsForMultiplicators");

            migrationBuilder.AddColumn<bool>(
                name: "IfNeedToConsiderTheDynamics",
                table: "Multiplicators",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
