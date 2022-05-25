using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class confs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ContactId",
                table: "Tags",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Contacts_ContactId",
                table: "Tags",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Contacts_ContactId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ContactId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contacts");
        }
    }
}
