using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolStandardAPI.Migrations
{
    public partial class UnitStatsReworked2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAtkRange",
                table: "UnitStatistics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "CurrentAtkRange",
                table: "UnitStatistics",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }
    }
}
