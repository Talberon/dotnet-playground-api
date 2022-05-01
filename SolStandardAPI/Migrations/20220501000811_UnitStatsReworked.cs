using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolStandardAPI.Migrations
{
    public partial class UnitStatsReworked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RetModifier",
                table: "UnitStatistics",
                newName: "Ret");

            migrationBuilder.RenameColumn(
                name: "MvModifier",
                table: "UnitStatistics",
                newName: "Mv");

            migrationBuilder.RenameColumn(
                name: "LuckModifier",
                table: "UnitStatistics",
                newName: "MaxCmd");

            migrationBuilder.RenameColumn(
                name: "CurrentHP",
                table: "UnitStatistics",
                newName: "Luck");

            migrationBuilder.RenameColumn(
                name: "CurrentCmd",
                table: "UnitStatistics",
                newName: "HP");

            migrationBuilder.RenameColumn(
                name: "CurrentArmor",
                table: "UnitStatistics",
                newName: "Blk");

            migrationBuilder.RenameColumn(
                name: "BlkModifier",
                table: "UnitStatistics",
                newName: "Atk");

            migrationBuilder.RenameColumn(
                name: "AtkModifier",
                table: "UnitStatistics",
                newName: "Armor");

            migrationBuilder.AddColumn<int[]>(
                name: "BaseAtkRange",
                table: "UnitStatistics",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseAtkRange",
                table: "UnitStatistics");

            migrationBuilder.RenameColumn(
                name: "Ret",
                table: "UnitStatistics",
                newName: "RetModifier");

            migrationBuilder.RenameColumn(
                name: "Mv",
                table: "UnitStatistics",
                newName: "MvModifier");

            migrationBuilder.RenameColumn(
                name: "MaxCmd",
                table: "UnitStatistics",
                newName: "LuckModifier");

            migrationBuilder.RenameColumn(
                name: "Luck",
                table: "UnitStatistics",
                newName: "CurrentHP");

            migrationBuilder.RenameColumn(
                name: "HP",
                table: "UnitStatistics",
                newName: "CurrentCmd");

            migrationBuilder.RenameColumn(
                name: "Blk",
                table: "UnitStatistics",
                newName: "CurrentArmor");

            migrationBuilder.RenameColumn(
                name: "Atk",
                table: "UnitStatistics",
                newName: "BlkModifier");

            migrationBuilder.RenameColumn(
                name: "Armor",
                table: "UnitStatistics",
                newName: "AtkModifier");
        }
    }
}
