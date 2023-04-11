using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class added_story : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryStep",
                table: "ModuleItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MyStoryTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyStoryTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TelegramId = table.Column<int>(type: "INTEGER", nullable: true),
                    MyStoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StoryItemType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryItems_ModuleItems_MyStoryId",
                        column: x => x.MyStoryId,
                        principalTable: "ModuleItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryItems_MyStoryId",
                table: "StoryItems",
                column: "MyStoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyStoryTemplates");

            migrationBuilder.DropTable(
                name: "StoryItems");

            migrationBuilder.DropColumn(
                name: "StoryStep",
                table: "ModuleItems");
        }
    }
}
