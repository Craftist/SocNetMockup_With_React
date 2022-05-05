using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocNetMockup.Migrations
{
    public partial class MigrationIdkForWhat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "GroupChatMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupChatRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupChatMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatRole_GroupChatMembers_GroupChatMemberId",
                        column: x => x.GroupChatMemberId,
                        principalTable: "GroupChatMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatRole_GroupChatMemberId",
                table: "GroupChatRole",
                column: "GroupChatMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatRole");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "GroupChatMembers");
        }
    }
}
