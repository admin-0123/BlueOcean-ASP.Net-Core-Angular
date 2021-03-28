using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtaApi.Migrations
{
    public partial class CategoriesValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Categories",
                newName: "Name");
        }
    }
}
