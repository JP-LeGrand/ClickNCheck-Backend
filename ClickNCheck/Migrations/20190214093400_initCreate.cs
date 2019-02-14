using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class initCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    TurnaraoundTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobProfile",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    isCompleted = table.Column<bool>(nullable: false),
                    OrganisationID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobProfile_Organisation_OrganisationID",
                        column: x => x.OrganisationID,
                        principalTable: "Organisation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    EmployeeNumber = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Otp = table.Column<int>(nullable: false),
                    ManagerID = table.Column<int>(nullable: false),
                    userID = table.Column<int>(nullable: true),
                    OrganisationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Organisation_OrganisationID",
                        column: x => x.OrganisationID,
                        principalTable: "Organisation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobProfile_Checks",
                columns: table => new
                {
                    JobProfileId = table.Column<int>(nullable: false),
                    ChecksId = table.Column<int>(nullable: false),
                    order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProfile_Checks", x => new { x.ChecksId, x.JobProfileId });
                    table.ForeignKey(
                        name: "FK_JobProfile_Checks_Checks_ChecksId",
                        column: x => x.ChecksId,
                        principalTable: "Checks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobProfile_Checks_JobProfile_JobProfileId",
                        column: x => x.JobProfileId,
                        principalTable: "JobProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ID_Passport = table.Column<string>(nullable: true),
                    ID_Type = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Maiden_Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
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
                        name: "FK_Candidate_User_RecruiterID",
                        column: x => x.RecruiterID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkCodes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Used = table.Column<bool>(nullable: false),
                    Admin_ID = table.Column<int>(nullable: false),
                    AdministratorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCodes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LinkCodes_User_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recruiter_JobProfile",
                columns: table => new
                {
                    JobProfileId = table.Column<int>(nullable: false),
                    RecruiterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiter_JobProfile", x => new { x.RecruiterId, x.JobProfileId });
                    table.ForeignKey(
                        name: "FK_Recruiter_JobProfile_JobProfile_JobProfileId",
                        column: x => x.JobProfileId,
                        principalTable: "JobProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruiter_JobProfile_User_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_OrganisationID",
                table: "Candidate",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruiterID",
                table: "Candidate",
                column: "RecruiterID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_JobProfile_JobProfileId",
                table: "Candidate_JobProfile",
                column: "JobProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfile_OrganisationID",
                table: "JobProfile",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfile_Checks_JobProfileId",
                table: "JobProfile_Checks",
                column: "JobProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCodes_AdministratorID",
                table: "LinkCodes",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Recruiter_JobProfile_JobProfileId",
                table: "Recruiter_JobProfile",
                column: "JobProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganisationID",
                table: "User",
                column: "OrganisationID");

            migrationBuilder.CreateIndex(
                name: "IX_User_userID",
                table: "User",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidate_JobProfile");

            migrationBuilder.DropTable(
                name: "JobProfile_Checks");

            migrationBuilder.DropTable(
                name: "LinkCodes");

            migrationBuilder.DropTable(
                name: "Recruiter_JobProfile");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "JobProfile");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Organisation");
        }
    }
}
