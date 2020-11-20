using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class Group2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Exams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_GroupId",
                table: "Exams",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Groups_GroupId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_GroupId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Exams");
        }
    }
}
