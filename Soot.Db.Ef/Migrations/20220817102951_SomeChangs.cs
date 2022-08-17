using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class SomeChangs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificaionId",
                schema: "Soot",
                table: "Notifications",
                newName: "NotificationId");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalSourceName",
                schema: "Soot",
                table: "ExternalContactMappings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationId",
                schema: "Soot",
                table: "Notifications",
                newName: "NotificaionId");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalSourceName",
                schema: "Soot",
                table: "ExternalContactMappings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
