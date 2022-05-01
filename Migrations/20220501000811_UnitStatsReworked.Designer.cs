﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using sol_standard_api.Models;

#nullable disable

namespace sol_standard_api.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20220501000811_UnitStatsReworked")]
    partial class UnitStatsReworked
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("sol_standard_api.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnitStatisticsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UnitStatisticsId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("sol_standard_api.Models.UnitStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Armor")
                        .HasColumnType("integer");

                    b.Property<int>("Atk")
                        .HasColumnType("integer");

                    b.Property<int[]>("BaseAtkRange")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int>("Blk")
                        .HasColumnType("integer");

                    b.Property<int[]>("CurrentAtkRange")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int>("HP")
                        .HasColumnType("integer");

                    b.Property<int>("Luck")
                        .HasColumnType("integer");

                    b.Property<int>("MaxCmd")
                        .HasColumnType("integer");

                    b.Property<int>("Mv")
                        .HasColumnType("integer");

                    b.Property<int>("Ret")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UnitStatistics");
                });

            modelBuilder.Entity("sol_standard_api.Models.Unit", b =>
                {
                    b.HasOne("sol_standard_api.Models.UnitStatistics", "UnitStatistics")
                        .WithMany()
                        .HasForeignKey("UnitStatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UnitStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
