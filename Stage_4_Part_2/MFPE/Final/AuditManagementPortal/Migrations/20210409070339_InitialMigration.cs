using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditManagementPortal.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditResponses",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false),
                    ProjectExecutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemedialActionDuration = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditResponses", x => x.AuditId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditResponses");
        }
    }
}
