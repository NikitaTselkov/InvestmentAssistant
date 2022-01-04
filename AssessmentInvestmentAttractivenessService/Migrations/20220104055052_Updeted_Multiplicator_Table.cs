using Microsoft.EntityFrameworkCore.Migrations;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    public partial class Updeted_Multiplicator_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsOfActivityOfCompanies_Multiplicators_MultiplicatorId",
                table: "FieldsOfActivityOfCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators");

            migrationBuilder.DropIndex(
                name: "IX_FieldsOfActivityOfCompanies_MultiplicatorId",
                table: "FieldsOfActivityOfCompanies");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Multiplicators");

            migrationBuilder.DropColumn(
                name: "MultiplicatorName",
                table: "Multiplicators");

            migrationBuilder.DropColumn(
                name: "MultiplicatorId",
                table: "FieldsOfActivityOfCompanies");

            migrationBuilder.AlterColumn<int>(
                name: "GroupOfMultiplicatorsId",
                table: "Multiplicators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Multiplicators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FieldOfActivityOfCompanyMultiplicator",
                columns: table => new
                {
                    DoesNotWorkWithCompaniesId = table.Column<int>(type: "int", nullable: false),
                    MultiplicatorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOfActivityOfCompanyMultiplicator", x => new { x.DoesNotWorkWithCompaniesId, x.MultiplicatorsId });
                    table.ForeignKey(
                        name: "FK_FieldOfActivityOfCompanyMultiplicator_FieldsOfActivityOfCompanies_DoesNotWorkWithCompaniesId",
                        column: x => x.DoesNotWorkWithCompaniesId,
                        principalTable: "FieldsOfActivityOfCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldOfActivityOfCompanyMultiplicator_Multiplicators_MultiplicatorsId",
                        column: x => x.MultiplicatorsId,
                        principalTable: "Multiplicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Multiplicators_DescriptionId",
                table: "Multiplicators",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldOfActivityOfCompanyMultiplicator_MultiplicatorsId",
                table: "FieldOfActivityOfCompanyMultiplicator",
                column: "MultiplicatorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Multiplicators_DescriptionsForMultiplicators_DescriptionId",
                table: "Multiplicators",
                column: "DescriptionId",
                principalTable: "DescriptionsForMultiplicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators",
                column: "GroupOfMultiplicatorsId",
                principalTable: "GroupsOfMultiplicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Multiplicators_DescriptionsForMultiplicators_DescriptionId",
                table: "Multiplicators");

            migrationBuilder.DropForeignKey(
                name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators");

            migrationBuilder.DropTable(
                name: "FieldOfActivityOfCompanyMultiplicator");

            migrationBuilder.DropIndex(
                name: "IX_Multiplicators_DescriptionId",
                table: "Multiplicators");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Multiplicators");

            migrationBuilder.AlterColumn<int>(
                name: "GroupOfMultiplicatorsId",
                table: "Multiplicators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Multiplicators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MultiplicatorName",
                table: "Multiplicators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MultiplicatorId",
                table: "FieldsOfActivityOfCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldsOfActivityOfCompanies_MultiplicatorId",
                table: "FieldsOfActivityOfCompanies",
                column: "MultiplicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsOfActivityOfCompanies_Multiplicators_MultiplicatorId",
                table: "FieldsOfActivityOfCompanies",
                column: "MultiplicatorId",
                principalTable: "Multiplicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators",
                column: "GroupOfMultiplicatorsId",
                principalTable: "GroupsOfMultiplicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
