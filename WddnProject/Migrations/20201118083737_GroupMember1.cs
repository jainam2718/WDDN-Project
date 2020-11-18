using Microsoft.EntityFrameworkCore.Migrations;

namespace WDDNProject.Migrations
{
    public partial class GroupMember1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupMemberId",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupMemberId",
                table: "Groups",
                column: "GroupMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupMembers_GroupMemberId",
                table: "Groups",
                column: "GroupMemberId",
                principalTable: "GroupMembers",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupMembers_GroupMemberId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupMemberId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupMemberId",
                table: "Groups");
        }
    }
}
