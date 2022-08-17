using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Soot");
            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Soot",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InboxId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MobileNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    WebSocket = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Soot",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "ContactTags",
                schema: "Soot",
                columns: table => new
                {
                    ContactTagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactId = table.Column<int>(type: "integer", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTags", x => x.ContactTagId);
                    table.ForeignKey(
                        name: "FK_ContactTags_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "Soot",
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalContactMappings",
                schema: "Soot",
                columns: table => new
                {
                    ExternalContactId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactId = table.Column<int>(type: "integer", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: false),
                    ExternalSourceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalContactMappings", x => x.ExternalContactId);
                    table.ForeignKey(
                        name: "FK_ExternalContactMappings_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "Soot",
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                schema: "Soot",
                columns: table => new
                {
                    InboxId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.InboxId);
                    table.ForeignKey(
                        name: "FK_Inbox_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "Soot",
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "Soot",
                columns: table => new
                {
                    NotificaionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Body = table.Column<string>(type: "text", nullable: false),
                    ContactId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificaionId);
                    table.ForeignKey(
                        name: "FK_Notifications_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "Soot",
                        principalTable: "Contacts",
                        principalColumn: "ContactId");
                });

            migrationBuilder.CreateTable(
                name: "InboxItems",
                schema: "Soot",
                columns: table => new
                {
                    InboxItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    InboxId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxItems", x => x.InboxItemId);
                    table.ForeignKey(
                        name: "FK_InboxItems_Inbox_InboxId",
                        column: x => x.InboxId,
                        principalSchema: "Soot",
                        principalTable: "Inbox",
                        principalColumn: "InboxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InboxItems_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "Soot",
                        principalTable: "Notifications",
                        principalColumn: "NotificaionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendActions",
                schema: "Soot",
                columns: table => new
                {
                    SendActionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<int>(type: "integer", nullable: false),
                    SendType = table.Column<int>(type: "integer", nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    IsDeliveryRequested = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendActions", x => x.SendActionId);
                    table.ForeignKey(
                        name: "FK_SendActions_Contacts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "Soot",
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SendActions_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "Soot",
                        principalTable: "Notifications",
                        principalColumn: "NotificaionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InboxItemActions",
                schema: "Soot",
                columns: table => new
                {
                    InboxItemActionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InboxItemId = table.Column<int>(type: "integer", nullable: false),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxItemActions", x => x.InboxItemActionId);
                    table.ForeignKey(
                        name: "FK_InboxItemActions_InboxItems_InboxItemId",
                        column: x => x.InboxItemId,
                        principalSchema: "Soot",
                        principalTable: "InboxItems",
                        principalColumn: "InboxItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendResults",
                schema: "Soot",
                columns: table => new
                {
                    SendResultId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SendActionId = table.Column<int>(type: "integer", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    IsRetry = table.Column<bool>(type: "boolean", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendResults", x => x.SendResultId);
                    table.ForeignKey(
                        name: "FK_SendResults_SendActions_SendActionId",
                        column: x => x.SendActionId,
                        principalSchema: "Soot",
                        principalTable: "SendActions",
                        principalColumn: "SendActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactTags_ContactId",
                schema: "Soot",
                table: "ContactTags",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalContactMappings_ContactId",
                schema: "Soot",
                table: "ExternalContactMappings",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_ContactId",
                schema: "Soot",
                table: "Inbox",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InboxItemActions_InboxItemId",
                schema: "Soot",
                table: "InboxItemActions",
                column: "InboxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_InboxId",
                schema: "Soot",
                table: "InboxItems",
                column: "InboxId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_NotificationId",
                schema: "Soot",
                table: "InboxItems",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContactId",
                schema: "Soot",
                table: "Notifications",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SendActions_NotificationId",
                schema: "Soot",
                table: "SendActions",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SendActions_ReceiverId",
                schema: "Soot",
                table: "SendActions",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_SendResults_SendActionId",
                schema: "Soot",
                table: "SendResults",
                column: "SendActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactTags",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "ExternalContactMappings",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "InboxItemActions",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "SendResults",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "InboxItems",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "SendActions",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "Inbox",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "Soot");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Soot");
        }
    }
}
