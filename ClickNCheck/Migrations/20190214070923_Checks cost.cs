using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class Checkscost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Checks",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Checks");
        }
    }
}
