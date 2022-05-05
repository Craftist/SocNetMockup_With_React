using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocNetMockup.Migrations
{
    public partial class AddMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PmPeers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmPeers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PmPeers_Image_CoverId",
                        column: x => x.CoverId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatMessages_GroupChatMembers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "GroupChatMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupChatMessages_GroupChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupChatMessages_PmPeers_PeerId",
                        column: x => x.PeerId,
                        principalTable: "PmPeers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PmPeerUser",
                columns: table => new
                {
                    MembersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PmPeerUser", x => new { x.MembersId, x.PeersId });
                    table.ForeignKey(
                        name: "FK_PmPeerUser_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PmPeerUser_PmPeers_PeersId",
                        column: x => x.PeersId,
                        principalTable: "PmPeers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessages_ChatId",
                table: "GroupChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessages_PeerId",
                table: "GroupChatMessages",
                column: "PeerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMessages_SenderId",
                table: "GroupChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PmPeers_CoverId",
                table: "PmPeers",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_PmPeerUser_PeersId",
                table: "PmPeerUser",
                column: "PeersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatMessages");

            migrationBuilder.DropTable(
                name: "PmPeerUser");

            migrationBuilder.DropTable(
                name: "PmPeers");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
