using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTrackerApp.Migrations
{
    public partial class added_project_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectStatus",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectStatus",
                table: "Projects");
        }
    }
}
