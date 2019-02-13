using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickNCheck.Migrations
{
    public partial class addedcheckorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Checks",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "JobProfile_Checks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "JobProfile",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobProfile",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "TurnaraoundTime",
                table: "Checks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "JobProfile_Checks");

            migrationBuilder.DropColumn(
                name: "TurnaraoundTime",
                table: "Checks");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Checks",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "JobProfile",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "JobProfile",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
