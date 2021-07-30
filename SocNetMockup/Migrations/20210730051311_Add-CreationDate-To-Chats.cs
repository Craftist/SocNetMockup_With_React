using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocNetMockup.Migrations
{
    public partial class AddCreationDateToChats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_GroupChatMembers_OwnerId",
                table: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_GroupChats_OwnerId",
                table: "GroupChats");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "GroupChats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "GroupChats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "GroupChats");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "GroupChats",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
