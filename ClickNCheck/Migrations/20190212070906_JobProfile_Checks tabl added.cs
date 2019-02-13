using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class JobProfile_Checkstabladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobProfile_Checks",
                columns: table => new
                {
                    JobProfileId = table.Column<int>(nullable: false),
                    ChecksId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_JobProfile_Checks_JobProfileId",
                table: "JobProfile_Checks",
                column: "JobProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobProfile_Checks");
        }
    }
}
