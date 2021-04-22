using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIPracticeCheckProject.Migrations
{
    public partial class WebAPIPracticeCheckProjectModelsMenuItemOperationContextInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DateOfLaunch = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    isFreeDelivery = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "DateOfLaunch", "Name", "Price", "isActive", "isFreeDelivery" },
                values: new object[] { 1, 1, new DateTime(2021, 1, 20, 0, 3, 0, 0, DateTimeKind.Unspecified), "Poori", 20.0, true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");
        }
    }
}
