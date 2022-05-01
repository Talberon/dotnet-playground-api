using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sol_standard_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentHP = table.Column<int>(type: "integer", nullable: false),
                    CurrentArmor = table.Column<int>(type: "integer", nullable: false),
                    CurrentCmd = table.Column<int>(type: "integer", nullable: false),
                    CurrentAtkRange = table.Column<int[]>(type: "integer[]", nullable: false),
                    AtkModifier = table.Column<int>(type: "integer", nullable: false),
                    RetModifier = table.Column<int>(type: "integer", nullable: false),
                    LuckModifier = table.Column<int>(type: "integer", nullable: false),
                    MvModifier = table.Column<int>(type: "integer", nullable: false),
                    BlkModifier = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<string>(type: "text", nullable: false),
                    UnitStatisticsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitStatistics_UnitStatisticsId",
                        column: x => x.UnitStatisticsId,
                        principalTable: "UnitStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitStatisticsId",
                table: "Units",
                column: "UnitStatisticsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "UnitStatistics");
        }
    }
}
