using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestHouse.Infrastructure.Migrations
{
    public partial class createdAtTestRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TestRuns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "TestCases",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TestRuns");

            migrationBuilder.AlterColumn<long>(
                name: "Order",
                table: "TestCases",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
