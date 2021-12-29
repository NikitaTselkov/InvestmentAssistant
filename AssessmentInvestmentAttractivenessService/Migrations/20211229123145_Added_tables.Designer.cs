﻿// <auto-generated />
using System;
using AssessmentInvestmentAttractivenessService.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssessmentInvestmentAttractivenessService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211229123145_Added_tables")]
    partial class Added_tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.ToTable("DescriptionsForMultiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.FieldOfActivityOfCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FieldOfActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MultiplicatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MultiplicatorId");

                    b.ToTable("FieldsOfActivityOfCompanies");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.GroupOfMultiplicators", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GroupsOfMultiplicators");
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupOfMultiplicatorsId")
                        .HasColumnType("int");

                    b.Property<bool>("IfNeedToConsiderTheDynamics")
                        .HasColumnType("bit");

                    b.Property<string>("MultiplicatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("GroupOfMultiplicatorsId");

                    b.ToTable("Multiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.FieldOfActivityOfCompany", b =>
                {
                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.Multiplicator", null)
                        .WithMany("DoesNotWorkWithCompanies")
                        .HasForeignKey("MultiplicatorId");
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

                    b.HasOne("AssessmentInvestmentAttractivenessService.Models.GroupOfMultiplicators", "GroupOfMultiplicators")
                        .WithMany()
                        .HasForeignKey("GroupOfMultiplicatorsId");

                    b.Navigation("Company");

                    b.Navigation("GroupOfMultiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Company", b =>
                {
                    b.Navigation("Multiplicators");
                });

            modelBuilder.Entity("AssessmentInvestmentAttractivenessService.Models.Multiplicator", b =>
                {
                    b.Navigation("DoesNotWorkWithCompanies");

                    b.Navigation("Indexes");
                });
#pragma warning restore 612, 618
        }
    }
}
