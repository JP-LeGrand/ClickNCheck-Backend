using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class Removedorganisationfromchecks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Organisation_OrganisationID",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_OrganisationID",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "OrganisationID",
                table: "Checks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationID",
                table: "Checks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_OrganisationID",
                table: "Checks",
                column: "OrganisationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Organisation_OrganisationID",
                table: "Checks",
                column: "OrganisationID",
                principalTable: "Organisation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
