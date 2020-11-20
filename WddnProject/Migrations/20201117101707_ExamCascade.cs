using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class ExamCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmail",
                table: "Exams");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmail",
                table: "Exams",
                column: "AppEmail",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmail",
                table: "Exams");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmail",
                table: "Exams",
                column: "AppEmail",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
