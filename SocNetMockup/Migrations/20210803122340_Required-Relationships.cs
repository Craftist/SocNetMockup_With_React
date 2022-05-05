using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocNetMockup.Migrations
{
    public partial class RequiredRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatMembers_AspNetUsers_UserId",
                table: "GroupChatMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatMembers_GroupChats_ChatId",
                table: "GroupChatMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "GroupChatMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "GroupChatMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatMembers_AspNetUsers_UserId",
                table: "GroupChatMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatMembers_GroupChats_ChatId",
                table: "GroupChatMembers",
                column: "ChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatMembers_AspNetUsers_UserId",
                table: "GroupChatMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatMembers_GroupChats_ChatId",
                table: "GroupChatMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "GroupChatMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatId",
                table: "GroupChatMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatMembers_AspNetUsers_UserId",
                table: "GroupChatMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatMembers_GroupChats_ChatId",
                table: "GroupChatMembers",
                column: "ChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
