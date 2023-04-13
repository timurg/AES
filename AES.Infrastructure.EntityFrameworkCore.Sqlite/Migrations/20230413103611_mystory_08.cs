using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class mystory_08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemIndex",
                table: "StoryItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemIndex",
                table: "MyStoryTemplateItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemIndex",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "ItemIndex",
                table: "MyStoryTemplateItems");
        }
    }
}
