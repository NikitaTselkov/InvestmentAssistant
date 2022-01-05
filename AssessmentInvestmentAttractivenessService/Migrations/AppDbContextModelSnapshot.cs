﻿// <auto-generated />
using AssessmentInvestmentAttractivenessService.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.DescriptionForMultiplicators", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowToInterpret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DescriptionsForMultiplicators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Показывает, сколько лет должна проработать компания, получая ту же прибыль, чтобы окупить акционерный капитал.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "P/E"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Показывает, сколько вы платите за 1 рубль выручки компании.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "P/S"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Показывает, сколько денег можно выручить за компанию, если продать все ее имущество.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "P/B"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Позволяет оценить, переплачивает ли инвестор за остаток, который ему достанется, если компания обанкротится.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "P/BV"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Показывает, сколько заёмных средств приходится на каждый рубль собственного капитала.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "D/E"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Показывает, сколько прибылей до уплаты процентов, налогов и амортизации должна заработать компания, чтобы окупить реальную рыночную цену компании.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "EV/EBIDA"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Сообщает о достаточности или нехватке ресурсов для погашения займов и процентных ставок по ним.",
                            HowToInterpret = "Чем меньше, тем лучше",
                            Name = "DEBT/EBIDA"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Показывает, как компания генерирует чистую прибыль за счёт собственных средств, по которым компания не выплачивает проценты.",
                            HowToInterpret = "Чем больше, тем лучше",
                            Name = "ROE"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Показывает, как компания использует все активы, в том числе и заёмные, для получения прибыли.",
                            HowToInterpret = "Чем больше, тем лучше",
                            Name = "ROA"
                        });
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.FieldOfActivityOfCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FieldOfActivityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldOfActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FieldsOfActivityOfCompanies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FieldOfActivityCode = "HEAL",
                            FieldOfActivityName = "Здравоохранение"
                        },
                        new
                        {
                            Id = 2,
                            FieldOfActivityCode = "IT",
                            FieldOfActivityName = "Информационные технологии"
                        },
                        new
                        {
                            Id = 3,
                            FieldOfActivityCode = "COMS",
                            FieldOfActivityName = "Коммуникационные услуги"
                        },
                        new
                        {
                            Id = 4,
                            FieldOfActivityCode = "COMMS",
                            FieldOfActivityName = "Коммуникационные услуги"
                        },
                        new
                        {
                            Id = 5,
                            FieldOfActivityCode = "MATE",
                            FieldOfActivityName = "Материалы"
                        },
                        new
                        {
                            Id = 6,
                            FieldOfActivityCode = "REAL",
                            FieldOfActivityName = "Недвижимость"
                        },
                        new
                        {
                            Id = 7,
                            FieldOfActivityCode = "DCG",
                            FieldOfActivityName = "Потребительские товары длительного пользования"
                        },
                        new
                        {
                            Id = 8,
                            FieldOfActivityCode = "NDCG",
                            FieldOfActivityName = "Потребительские товары не длительного пользования"
                        },
                        new
                        {
                            Id = 9,
                            FieldOfActivityCode = "IND",
                            FieldOfActivityName = "Промышленность"
                        },
                        new
                        {
                            Id = 10,
                            FieldOfActivityCode = "FIN",
                            FieldOfActivityName = "Финансы"
                        },
                        new
                        {
                            Id = 11,
                            FieldOfActivityCode = "ENE",
                            FieldOfActivityName = "Энергетика"
                        });
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.GroupOfMultiplicators", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GroupsOfMultiplicators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GroupCode = "REVEM",
                            GroupName = "Доходный мультипликатор"
                        },
                        new
                        {
                            Id = 2,
                            GroupCode = "BALANM",
                            GroupName = "Балансовый мультипликатор"
                        },
                        new
                        {
                            Id = 3,
                            GroupCode = "PROFIM",
                            GroupName = "Мультипликатор рентабельности"
                        },
                        new
                        {
                            Id = 4,
                            GroupCode = "FSSM",
                            GroupName = "Мультипликатор финансовой устойчивости и платёжеспособности"
                        });
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Index", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MultiplicatorId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MultiplicatorId");

                    b.ToTable("Indexes");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Multiplicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("DescriptionId")
                        .HasColumnType("int");

                    b.Property<int>("GroupOfMultiplicatorsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DescriptionId");

                    b.HasIndex("GroupOfMultiplicatorsId");

                    b.ToTable("Multiplicators");
                });

            modelBuilder.Entity("FieldOfActivityOfCompanyMultiplicator", b =>
                {
                    b.Property<int>("DoesNotWorkWithCompaniesId")
                        .HasColumnType("int");

                    b.Property<int>("MultiplicatorsId")
                        .HasColumnType("int");

                    b.HasKey("DoesNotWorkWithCompaniesId", "MultiplicatorsId");

                    b.HasIndex("MultiplicatorsId");

                    b.ToTable("FieldOfActivityOfCompanyMultiplicator");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Index", b =>
                {
                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.Multiplicator", "Multiplicator")
                        .WithMany("Indexes")
                        .HasForeignKey("MultiplicatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Multiplicator");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Multiplicator", b =>
                {
                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.Company", "Company")
                        .WithMany("Multiplicators")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.DescriptionForMultiplicators", "Description")
                        .WithMany("Multiplicators")
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.GroupOfMultiplicators", "GroupOfMultiplicators")
                        .WithMany("Multiplicators")
                        .HasForeignKey("GroupOfMultiplicatorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Description");

                    b.Navigation("GroupOfMultiplicators");
                });

            modelBuilder.Entity("FieldOfActivityOfCompanyMultiplicator", b =>
                {
                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.FieldOfActivityOfCompany", null)
                        .WithMany()
                        .HasForeignKey("DoesNotWorkWithCompaniesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.Multiplicator", null)
                        .WithMany()
                        .HasForeignKey("MultiplicatorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Company", b =>
                {
                    b.Navigation("Multiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.DescriptionForMultiplicators", b =>
                {
                    b.Navigation("Multiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.GroupOfMultiplicators", b =>
                {
                    b.Navigation("Multiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Multiplicator", b =>
                {
                    b.Navigation("Indexes");
                });
#pragma warning restore 612, 618
        }
    }
}
