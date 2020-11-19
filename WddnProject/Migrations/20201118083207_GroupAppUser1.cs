using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class GroupAppUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AppUserId",
                table: "Groups",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_AppUserId",
                table: "Groups",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_AppUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_AppUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Groups");
        }
    }
}
