using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstAPIProject.Migrations
{
    public partial class MyFirstAPIProjectModelsCustomerManagementSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "IsOldCustomer", "Name", "Phone" },
                values: new object[] { 1, false, "Ramu", "9876543210" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);
        }
    }
}
