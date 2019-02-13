using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class ManagerTableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Administrator_administratorID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruiter_Administrator_ManagerID",
                table: "Recruiter");

            migrationBuilder.DropIndex(
                name: "IX_Administrator_administratorID",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "administratorID",
                table: "Administrator");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerID",
                table: "Administrator",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Candidate_JobProfile",
                columns: table => new
                {
                    JobProfileId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate_JobProfile", x => new { x.CandidateId, x.JobProfileId });
                    table.ForeignKey(
                        name: "FK_Candidate_JobProfile_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidate_JobProfile_JobProfile_JobProfileId",
                        column: x => x.JobProfileId,
                        principalTable: "JobProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    Password = table.Column<int>(nullable: false),
                    OrganisationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Manager_Organisation_OrganisationID",
                        column: x => x.OrganisationID,
                        principalTable: "Organisation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_ManagerID",
                table: "Administrator",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_JobProfile_JobProfileId",
                table: "Candidate_JobProfile",
                column: "JobProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_OrganisationID",
                table: "Manager",
                column: "OrganisationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Manager_ManagerID",
                table: "Administrator",
                column: "ManagerID",
                principalTable: "Manager",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruiter_Manager_ManagerID",
                table: "Recruiter",
                column: "ManagerID",
                principalTable: "Manager",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Manager_ManagerID",
                table: "Administrator");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruiter_Manager_ManagerID",
                table: "Recruiter");

            migrationBuilder.DropTable(
                name: "Candidate_JobProfile");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropIndex(
                name: "IX_Administrator_ManagerID",
                table: "Administrator");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerID",
                table: "Administrator",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "administratorID",
                table: "Administrator",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_administratorID",
                table: "Administrator",
                column: "administratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Administrator_administratorID",
                table: "Administrator",
                column: "administratorID",
                principalTable: "Administrator",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruiter_Administrator_ManagerID",
                table: "Recruiter",
                column: "ManagerID",
                principalTable: "Administrator",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
