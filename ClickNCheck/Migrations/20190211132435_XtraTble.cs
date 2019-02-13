using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class XtraTble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrganisationID",
                table: "Recruiter",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ManagerID",
                table: "Recruiter",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationID",
                table: "JobProfile",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationID",
                table: "Checks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ManagerID",
                table: "Administrator",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "administratorID",
                table: "Administrator",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ID_Passport = table.Column<string>(nullable: false),
                    ID_Type = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Maiden_Surname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    OrganisationID = table.Column<int>(nullable: true),
                    RecruiterID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Candidate_Organisation_OrganisationID",
                        column: x => x.OrganisationID,
                        principalTable: "Organisation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidate_Recruiter_RecruiterID",
                        column: x => x.RecruiterID,
                        principalTable: "Recruiter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_ManagerID",
                table: "Recruiter",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_OrganisationID",
                table: "Recruiter",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfile_OrganisationID",
                table: "JobProfile",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_OrganisationID",
                table: "Checks",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_OrganisationID",
                table: "Administrator",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_administratorID",
                table: "Administrator",
                column: "administratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_OrganisationID",
                table: "Candidate",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruiterID",
                table: "Candidate",
                column: "RecruiterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Organisation_OrganisationID",
                table: "Administrator",
                column: "OrganisationID",
                principalTable: "Organisation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Administrator_administratorID",
                table: "Administrator",
                column: "administratorID",
                principalTable: "Administrator",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Organisation_OrganisationID",
                table: "Checks",
                column: "OrganisationID",
                principalTable: "Organisation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobProfile_Organisation_OrganisationID",
                table: "JobProfile",
                column: "OrganisationID",
                principalTable: "Organisation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruiter_Administrator_ManagerID",
                table: "Recruiter",
                column: "ManagerID",
                principalTable: "Administrator",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruiter_Organisation_OrganisationID",
                table: "Recruiter",
                column: "OrganisationID",
                principalTable: "Organisation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Organisation_OrganisationID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Administrator_administratorID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Organisation_OrganisationID",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_JobProfile_Organisation_OrganisationID",
                table: "JobProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruiter_Administrator_ManagerID",
                table: "Recruiter");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruiter_Organisation_OrganisationID",
                table: "Recruiter");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Recruiter_ManagerID",
                table: "Recruiter");

            migrationBuilder.DropIndex(
                name: "IX_Recruiter_OrganisationID",
                table: "Recruiter");

            migrationBuilder.DropIndex(
                name: "IX_JobProfile_OrganisationID",
                table: "JobProfile");

            migrationBuilder.DropIndex(
                name: "IX_Checks_OrganisationID",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Administrator_OrganisationID",
                table: "Administrator");

            migrationBuilder.DropIndex(
                name: "IX_Administrator_administratorID",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "ManagerID",
                table: "Recruiter");

            migrationBuilder.DropColumn(
                name: "ManagerID",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "administratorID",
                table: "Administrator");

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationID",
                table: "Recruiter",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationID",
                table: "JobProfile",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganisationID",
                table: "Checks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
