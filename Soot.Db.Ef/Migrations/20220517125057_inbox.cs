using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class inbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InboxId = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WebSocket = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "ExternalContactMappings",
                columns: table => new
                {
                    ExternalContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalSourceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalContactMappings", x => x.ExternalContactId);
                    table.ForeignKey(
                        name: "FK_ExternalContactMappings_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                columns: table => new
                {
                    InboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.InboxId);
                    table.ForeignKey(
                        name: "FK_Inbox_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificaionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Types = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificaionId);
                    table.ForeignKey(
                        name: "FK_Notifications_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId");
                });

            migrationBuilder.CreateTable(
                name: "InboxItems",
                columns: table => new
                {
                    InboxItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    InboxId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NotificationNotificaionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxItems", x => x.InboxItemId);
                    table.ForeignKey(
                        name: "FK_InboxItems_Inbox_InboxId",
                        column: x => x.InboxId,
                        principalTable: "Inbox",
                        principalColumn: "InboxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InboxItems_Notifications_NotificationNotificaionId",
                        column: x => x.NotificationNotificaionId,
                        principalTable: "Notifications",
                        principalColumn: "NotificaionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendActions",
                columns: table => new
                {
                    SendActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    SendType = table.Column<int>(type: "int", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    NotificationNotificaionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendActions", x => x.SendActionId);
                    table.ForeignKey(
                        name: "FK_SendActions_Contacts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SendActions_Notifications_NotificationNotificaionId",
                        column: x => x.NotificationNotificaionId,
                        principalTable: "Notifications",
                        principalColumn: "NotificaionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InboxItemActions",
                columns: table => new
                {
                    InboxItemActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InboxItemId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxItemActions", x => x.InboxItemActionId);
                    table.ForeignKey(
                        name: "FK_InboxItemActions_InboxItems_InboxItemId",
                        column: x => x.InboxItemId,
                        principalTable: "InboxItems",
                        principalColumn: "InboxItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendResults",
                columns: table => new
                {
                    SendResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendActionId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    IsRetry = table.Column<bool>(type: "bit", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendResults", x => x.SendResultId);
                    table.ForeignKey(
                        name: "FK_SendResults_SendActions_SendActionId",
                        column: x => x.SendActionId,
                        principalTable: "SendActions",
                        principalColumn: "SendActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalContactMappings_ContactId",
                table: "ExternalContactMappings",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_ContactId",
                table: "Inbox",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InboxItemActions_InboxItemId",
                table: "InboxItemActions",
                column: "InboxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_InboxId",
                table: "InboxItems",
                column: "InboxId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_NotificationNotificaionId",
                table: "InboxItems",
                column: "NotificationNotificaionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContactId",
                table: "Notifications",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SendActions_NotificationNotificaionId",
                table: "SendActions",
                column: "NotificationNotificaionId");

            migrationBuilder.CreateIndex(
                name: "IX_SendActions_ReceiverId",
                table: "SendActions",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_SendResults_SendActionId",
                table: "SendResults",
                column: "SendActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalContactMappings");

            migrationBuilder.DropTable(
                name: "InboxItemActions");

            migrationBuilder.DropTable(
                name: "SendResults");

            migrationBuilder.DropTable(
                name: "InboxItems");

            migrationBuilder.DropTable(
                name: "SendActions");

            migrationBuilder.DropTable(
                name: "Inbox");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
