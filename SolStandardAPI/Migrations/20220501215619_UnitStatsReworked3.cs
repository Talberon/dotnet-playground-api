using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolStandardAPI.Migrations
{
    public partial class UnitStatsReworked3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BaseAtkRange",
                table: "UnitStatistics",
                type: "text",
                nullable: false,
                oldClrType: typeof(int[]),
                oldType: "integer[]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int[]>(
                name: "BaseAtkRange",
                table: "UnitStatistics",
                type: "integer[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
