using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class ExamCreateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_AppUserId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_AppUserId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "AppEmailId",
                table: "Exams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_AppEmailId",
                table: "Exams",
                column: "AppEmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmailId",
                table: "Exams",
                column: "AppEmailId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmailId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_AppEmailId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AppEmailId",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_AppUserId",
                table: "Exams",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_AppUserId",
                table: "Exams",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
