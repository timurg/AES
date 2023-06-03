using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class venue01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "StoryItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "StoryItems",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "StoryItems",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "StoryItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "MyStoryTemplateItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "MyStoryTemplateItems",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "MyStoryTemplateItems",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "MyStoryTemplateItems");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "MyStoryTemplateItems");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "MyStoryTemplateItems");
        }
    }
}
