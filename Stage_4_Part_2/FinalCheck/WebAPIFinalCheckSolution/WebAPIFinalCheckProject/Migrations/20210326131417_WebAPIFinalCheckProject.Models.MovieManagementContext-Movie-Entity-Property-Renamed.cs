using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIFinalCheckProject.Migrations
{
    public partial class WebAPIFinalCheckProjectModelsMovieManagementContextMovieEntityPropertyRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActivce",
                table: "Movies",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Movies",
                newName: "IsActivce");
        }
    }
}
