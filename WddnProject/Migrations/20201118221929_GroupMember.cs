using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class GroupMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserGroupMember_AspNetUsers_AppUserId",
                table: "AppUserGroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserGroupMember_GroupMembers_GroupMemberId",
                table: "AppUserGroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupMembers_GroupMemberId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupMemberId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserGroupMember",
                table: "AppUserGroupMember");

            migrationBuilder.RenameTable(
                name: "AppUserGroupMember",
                newName: "AppUserGroupMembers");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserGroupMember_GroupMemberId",
                table: "AppUserGroupMembers",
                newName: "IX_AppUserGroupMembers_GroupMemberId");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GroupMembers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserGroupMembers",
                table: "AppUserGroupMembers",
                columns: new[] { "AppUserId", "GroupMemberId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserGroupMembers_AspNetUsers_AppUserId",
                table: "AppUserGroupMembers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserGroupMembers_GroupMembers_GroupMemberId",
                table: "AppUserGroupMembers",
                column: "GroupMemberId",
                principalTable: "GroupMembers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserGroupMembers_AspNetUsers_AppUserId",
                table: "AppUserGroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserGroupMembers_GroupMembers_GroupMemberId",
                table: "AppUserGroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_Groups_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserGroupMembers",
                table: "AppUserGroupMembers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupMembers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GroupMembers");

            migrationBuilder.RenameTable(
                name: "AppUserGroupMembers",
                newName: "AppUserGroupMember");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserGroupMembers_GroupMemberId",
                table: "AppUserGroupMember",
                newName: "IX_AppUserGroupMember_GroupMemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserGroupMember",
                table: "AppUserGroupMember",
                columns: new[] { "AppUserId", "GroupMemberId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupMemberId",
                table: "Groups",
                column: "GroupMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserGroupMember_AspNetUsers_AppUserId",
                table: "AppUserGroupMember",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserGroupMember_GroupMembers_GroupMemberId",
                table: "AppUserGroupMember",
                column: "GroupMemberId",
                principalTable: "GroupMembers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupMembers_GroupMemberId",
                table: "Groups",
                column: "GroupMemberId",
                principalTable: "GroupMembers",
                principalColumn: "id");
        }
    }
}
