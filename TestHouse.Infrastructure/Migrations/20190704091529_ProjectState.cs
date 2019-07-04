using Microsoft.EntityFrameworkCore.Migrations;

namespace TestHouse.Infrastructure.Migrations
{
    public partial class ProjectState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Projects");
        }
    }
}
