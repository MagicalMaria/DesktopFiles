using Microsoft.EntityFrameworkCore.Migrations;

namespace ScreenAPIProject.Migrations
{
    public partial class ScreenAPIProjectModelsShowContextInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ScreenName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ScreenName);
                });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "ScreenName", "Capacity" },
                values: new object[] { "Kite", 30 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
