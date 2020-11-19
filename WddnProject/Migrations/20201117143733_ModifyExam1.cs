using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class ModifyExam1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmail",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_AppEmail",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "AppEmail",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Exams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exams",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Exams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Exams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Exams",
                nullable: false,
                defaultValue: "");

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "AppEmail",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_AppEmail",
                table: "Exams",
                column: "AppEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_AppEmail",
                table: "Exams",
                column: "AppEmail",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
