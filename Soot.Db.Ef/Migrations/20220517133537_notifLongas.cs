using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class notifLongas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Types",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Inbox");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeliveryRequested",
                table: "SendActions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeliveryRequested",
                table: "SendActions");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Types",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Inbox",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
