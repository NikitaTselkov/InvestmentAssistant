using Microsoft.EntityFrameworkCore.Migrations;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    public partial class Added_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionsForMultiplicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionsForMultiplicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupsOfMultiplicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsOfMultiplicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Multiplicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MultiplicatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupOfMultiplicatorsId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfNeedToConsiderTheDynamics = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multiplicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multiplicators_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Multiplicators_GroupsOfMultiplicators_GroupOfMultiplicatorsId",
                        column: x => x.GroupOfMultiplicatorsId,
                        principalTable: "GroupsOfMultiplicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldsOfActivityOfCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldOfActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultiplicatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldsOfActivityOfCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldsOfActivityOfCompanies_Multiplicators_MultiplicatorId",
                        column: x => x.MultiplicatorId,
                        principalTable: "Multiplicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Indexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    MultiplicatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indexes_Multiplicators_MultiplicatorId",
                        column: x => x.MultiplicatorId,
                        principalTable: "Multiplicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldsOfActivityOfCompanies_MultiplicatorId",
                table: "FieldsOfActivityOfCompanies",
                column: "MultiplicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Indexes_MultiplicatorId",
                table: "Indexes",
                column: "MultiplicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Multiplicators_CompanyId",
                table: "Multiplicators",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Multiplicators_GroupOfMultiplicatorsId",
                table: "Multiplicators",
                column: "GroupOfMultiplicatorsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescriptionsForMultiplicators");

            migrationBuilder.DropTable(
                name: "FieldsOfActivityOfCompanies");

            migrationBuilder.DropTable(
                name: "Indexes");

            migrationBuilder.DropTable(
                name: "Multiplicators");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "GroupsOfMultiplicators");
        }
    }
}
