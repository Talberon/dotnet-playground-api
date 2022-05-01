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
    [Migration("20220430231540_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<int>("AtkModifier")
                        .HasColumnType("integer");

                    b.Property<int>("BlkModifier")
                        .HasColumnType("integer");

                    b.Property<int>("CurrentArmor")
                        .HasColumnType("integer");

                    b.Property<int[]>("CurrentAtkRange")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int>("CurrentCmd")
                        .HasColumnType("integer");

                    b.Property<int>("CurrentHP")
                        .HasColumnType("integer");

                    b.Property<int>("LuckModifier")
                        .HasColumnType("integer");

                    b.Property<int>("MvModifier")
                        .HasColumnType("integer");

                    b.Property<int>("RetModifier")
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
