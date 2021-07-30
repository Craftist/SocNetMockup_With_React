using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocNetMockup.Migrations
{
    public partial class AddBasicGroupChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupChatMembers_GroupChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMembers_ChatId",
                table: "GroupChatMembers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMembers_UserId",
                table: "GroupChatMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_OwnerId",
                table: "GroupChats",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_GroupChatMembers_OwnerId",
                table: "GroupChats",
                column: "OwnerId",
                principalTable: "GroupChatMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatMembers_GroupChats_ChatId",
                table: "GroupChatMembers");

            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropTable(
                name: "GroupChatMembers");
        }
    }
}
