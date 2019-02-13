using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RecruiterID",
                table: "Candidate",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruiterID",
                table: "Candidate",
                column: "RecruiterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_RecruiterID",
                table: "Candidate",
                column: "RecruiterID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_RecruiterID",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_RecruiterID",
                table: "Candidate");

            migrationBuilder.AlterColumn<int>(
                name: "RecruiterID",
                table: "Candidate",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
