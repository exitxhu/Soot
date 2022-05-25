using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soot.Db.Ef.Migrations
{
    public partial class fixTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactTags_Tags_TagId",
                table: "ContactTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Contacts_ContactId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ContactId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_ContactTags_TagId",
                table: "ContactTags");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "ContactTags");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "ContactTags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "ContactTags");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "ContactTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ContactId",
                table: "Tags",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTags_TagId",
                table: "ContactTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactTags_Tags_TagId",
                table: "ContactTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Contacts_ContactId",
                table: "Tags",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "ContactId");
        }
    }
}
