using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleShowAPI.Migrations
{
    public partial class ScheduleShowAPIModelsInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ShowId);
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ShowId", "MovieID", "ScreenName", "Time" },
                values: new object[] { 1, 101, "Kite", "15:00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
