using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class notifLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboxItems_Notifications_NotificationNotificaionId",
                table: "InboxItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SendActions_Notifications_NotificationNotificaionId",
                table: "SendActions");

            migrationBuilder.DropIndex(
                name: "IX_SendActions_NotificationNotificaionId",
                table: "SendActions");

            migrationBuilder.DropIndex(
                name: "IX_InboxItems_NotificationNotificaionId",
                table: "InboxItems");

            migrationBuilder.DropColumn(
                name: "NotificationNotificaionId",
                table: "SendActions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationNotificaionId",
                table: "InboxItems");

            migrationBuilder.AlterColumn<long>(
                name: "NotificationId",
                table: "SendActions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "NotificationId",
                table: "InboxItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SendActions_NotificationId",
                table: "SendActions",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_NotificationId",
                table: "InboxItems",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InboxItems_Notifications_NotificationId",
                table: "InboxItems",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificaionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SendActions_Notifications_NotificationId",
                table: "SendActions",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "NotificaionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboxItems_Notifications_NotificationId",
                table: "InboxItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SendActions_Notifications_NotificationId",
                table: "SendActions");

            migrationBuilder.DropIndex(
                name: "IX_SendActions_NotificationId",
                table: "SendActions");

            migrationBuilder.DropIndex(
                name: "IX_InboxItems_NotificationId",
                table: "InboxItems");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "SendActions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "NotificationNotificaionId",
                table: "SendActions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "InboxItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "NotificationNotificaionId",
                table: "InboxItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SendActions_NotificationNotificaionId",
                table: "SendActions",
                column: "NotificationNotificaionId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_NotificationNotificaionId",
                table: "InboxItems",
                column: "NotificationNotificaionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InboxItems_Notifications_NotificationNotificaionId",
                table: "InboxItems",
                column: "NotificationNotificaionId",
                principalTable: "Notifications",
                principalColumn: "NotificaionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SendActions_Notifications_NotificationNotificaionId",
                table: "SendActions",
                column: "NotificationNotificaionId",
                principalTable: "Notifications",
                principalColumn: "NotificaionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
