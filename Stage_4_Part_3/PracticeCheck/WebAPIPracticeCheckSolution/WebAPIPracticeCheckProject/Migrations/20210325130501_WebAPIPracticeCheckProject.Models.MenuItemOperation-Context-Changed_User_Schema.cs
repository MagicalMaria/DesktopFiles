using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIPracticeCheckProject.Migrations
{
    public partial class WebAPIPracticeCheckProjectModelsMenuItemOperationContextChanged_User_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
