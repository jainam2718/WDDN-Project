using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class AppUserModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "id",
                table: "AppUserGroupMembers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupMemberId",
                table: "Groups",
                column: "GroupMemberId",
                unique: true,
                filter: "[GroupMemberId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupMembers_GroupMemberId",
                table: "Groups",
                column: "GroupMemberId",
                principalTable: "GroupMembers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupMembers_GroupMemberId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupMemberId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "AppUserGroupMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
