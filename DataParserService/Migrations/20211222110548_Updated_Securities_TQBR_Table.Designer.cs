﻿// <auto-generated />
using System;
using DataParserService.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataParserService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211222110548_Updated_Securities_TQBR_Table")]
    partial class Updated_Securities_TQBR_Table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataParserService.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectorLongName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectorShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecuritieTQBRId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SecuritieTQBRId")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DataParserService.Models.Index", b =>
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

            modelBuilder.Entity("DataParserService.Models.Multiplicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Multiplicators");
                });

            modelBuilder.Entity("DataParserService.Models.SecuritieTQBR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SECID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SECTYPE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Securities_TQBR");
                });

            modelBuilder.Entity("DataParserService.Models.Company", b =>
                {
                    b.HasOne("DataParserService.Models.SecuritieTQBR", "SecuritieTQBR")
                        .WithOne("Company")
                        .HasForeignKey("DataParserService.Models.Company", "SecuritieTQBRId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SecuritieTQBR");
                });

            modelBuilder.Entity("DataParserService.Models.Index", b =>
                {
                    b.HasOne("DataParserService.Models.Multiplicator", "Multiplicator")
                        .WithMany("Indexes")
                        .HasForeignKey("MultiplicatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Multiplicator");
                });

            modelBuilder.Entity("DataParserService.Models.Multiplicator", b =>
                {
                    b.HasOne("DataParserService.Models.Company", "Company")
                        .WithMany("Multiplicators")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("DataParserService.Models.Company", b =>
                {
                    b.Navigation("Multiplicators");
                });

            modelBuilder.Entity("DataParserService.Models.Multiplicator", b =>
                {
                    b.Navigation("Indexes");
                });

            modelBuilder.Entity("DataParserService.Models.SecuritieTQBR", b =>
                {
                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
