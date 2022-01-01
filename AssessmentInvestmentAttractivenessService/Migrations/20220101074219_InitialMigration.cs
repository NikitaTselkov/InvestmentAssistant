using Microsoft.EntityFrameworkCore.Migrations;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    public partial class InitialMigration : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    FieldOfActivityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.InsertData(
                table: "DescriptionsForMultiplicators",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Показывает, сколько лет должна проработать компания, получая ту же прибыль, чтобы окупить акционерный капитал.", "P/E" },
                    { 2, "Показывает, сколько вы платите за 1 рубль выручки компании.", "P/S" },
                    { 3, "Показывает, сколько денег можно выручить за компанию, если продать все ее имущество.", "P/B" },
                    { 4, "Позволяет оценить, переплачивает ли инвестор за остаток, который ему достанется, если компания обанкротится.", "P/BV" },
                    { 5, "Показывает, сколько заёмных средств приходится на каждый рубль собственного капитала.", "D/E" },
                    { 6, "Показывает, сколько прибылей до уплаты процентов, налогов и амортизации должна заработать компания, чтобы окупить реальную рыночную цену компании.", "EV/EBIDA" },
                    { 7, "Сообщает о достаточности или нехватке ресурсов для погашения займов и процентных ставок по ним.", "DEBT/EBIDA" },
                    { 8, "Показывает, как компания генерирует чистую прибыль за счёт собственных средств, по которым компания не выплачивает проценты.", "ROE" },
                    { 9, "Показывает, как компания использует все активы, в том числе и заёмные, для получения прибыли.", "ROA" }
                });

            migrationBuilder.InsertData(
                table: "FieldsOfActivityOfCompanies",
                columns: new[] { "Id", "FieldOfActivityCode", "FieldOfActivityName", "MultiplicatorId" },
                values: new object[,]
                {
                    { 11, "ENE", "Энергетика", null },
                    { 10, "FIN", "Финансы", null },
                    { 9, "IND", "Промышленность", null },
                    { 8, "NDCG", "Потребительские товары не длительного пользования", null },
                    { 7, "DCG", "Потребительские товары длительного пользования", null },
                    { 3, "COMS", "Коммуникационные услуги", null },
                    { 5, "MATE", "Материалы", null },
                    { 4, "COMMS", "Коммуникационные услуги", null },
                    { 2, "IT", "Информационные технологии", null },
                    { 1, "HEAL", "Здравоохранение", null },
                    { 6, "REAL", "Недвижимость", null }
                });

            migrationBuilder.InsertData(
                table: "GroupsOfMultiplicators",
                columns: new[] { "Id", "GroupCode", "GroupName" },
                values: new object[,]
                {
                    { 3, "PROFIM", "Мультипликатор рентабельности" },
                    { 1, "REVEM", "Доходный мультипликатор" },
                    { 2, "BALANM", "Балансовый мультипликатор" },
                    { 4, "FSSM", "Мультипликатор финансовой устойчивости и платёжеспособности" }
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
