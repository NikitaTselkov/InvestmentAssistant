using Microsoft.EntityFrameworkCore.Migrations;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    public partial class linked_Tables_GroupOfMultiplicators_And_DescriptionForMultiplicators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators");

            migrationBuilder.DropIndex(
                name: "IX_Multiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupsOfMultiplicators",
                table: "GroupsOfMultiplicators");

            migrationBuilder.DropColumn(
                name: "GroupOfMultiplicatorsId",
                table: "Multiplicators");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupsOfMultiplicators");

            migrationBuilder.AlterColumn<string>(
                name: "GroupCode",
                table: "GroupsOfMultiplicators",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupsOfMultiplicators",
                table: "GroupsOfMultiplicators",
                column: "GroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionsForMultiplicators_CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators",
                column: "CodeOfGroupOfMultiplicator");

            migrationBuilder.AddForeignKey(
                name: "FK_DescriptionsForMultiplicators_GroupsOfMultiplicators_CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators",
                column: "CodeOfGroupOfMultiplicator",
                principalTable: "GroupsOfMultiplicators",
                principalColumn: "GroupCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescriptionsForMultiplicators_GroupsOfMultiplicators_CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupsOfMultiplicators",
                table: "GroupsOfMultiplicators");

            migrationBuilder.DropIndex(
                name: "IX_DescriptionsForMultiplicators_CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators");

            migrationBuilder.DeleteData(
                table: "GroupsOfMultiplicators",
                keyColumn: "GroupCode",
                keyValue: "BALANM");

            migrationBuilder.DeleteData(
                table: "GroupsOfMultiplicators",
                keyColumn: "GroupCode",
                keyValue: "FSSM");

            migrationBuilder.DeleteData(
                table: "GroupsOfMultiplicators",
                keyColumn: "GroupCode",
                keyValue: "PROFIM");

            migrationBuilder.DeleteData(
                table: "GroupsOfMultiplicators",
                keyColumn: "GroupCode",
                keyValue: "REVEM");

            migrationBuilder.AddColumn<int>(
                name: "GroupOfMultiplicatorsId",
                table: "Multiplicators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "GroupCode",
                table: "GroupsOfMultiplicators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GroupsOfMultiplicators",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "CodeOfGroupOfMultiplicator",
                table: "DescriptionsForMultiplicators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupsOfMultiplicators",
                table: "GroupsOfMultiplicators",
                column: "Id");

            migrationBuilder.InsertData(
                table: "GroupsOfMultiplicators",
                columns: new[] { "Id", "GroupCode", "GroupName" },
                values: new object[,]
                {
                    { 1, "REVEM", "Доходный мультипликатор" },
                    { 2, "BALANM", "Балансовый мультипликатор" },
                    { 3, "PROFIM", "Мультипликатор рентабельности" },
                    { 4, "FSSM", "Мультипликатор финансовой устойчивости и платёжеспособности" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Multiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators",
                column: "GroupOfMultiplicatorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators",
                column: "GroupOfMultiplicatorsId",
                principalTable: "GroupsOfMultiplicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
