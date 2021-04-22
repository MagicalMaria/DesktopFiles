using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIPracticeCheckProject.Migrations
{
    public partial class WebAPIPracticeCheckProjectModelsMenuItemOperationContextCartSchemaChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_MenuItems_menuItemId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "menuItemId",
                table: "Carts",
                newName: "MenuItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_menuItemId",
                table: "Carts",
                newName: "IX_Carts_MenuItemID");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_MenuItems_MenuItemID",
                table: "Carts",
                column: "MenuItemID",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_MenuItems_MenuItemID",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "MenuItemID",
                table: "Carts",
                newName: "menuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_MenuItemID",
                table: "Carts",
                newName: "IX_Carts_menuItemId");

            migrationBuilder.AlterColumn<int>(
                name: "menuItemId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_MenuItems_menuItemId",
                table: "Carts",
                column: "menuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
