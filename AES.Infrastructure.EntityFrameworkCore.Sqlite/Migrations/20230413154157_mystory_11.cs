using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class mystory_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryPollItem");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "StoryItems",
                newName: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryItems_TemplateId",
                table: "StoryItems",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryItems_MyStoryTemplateItems_TemplateId",
                table: "StoryItems",
                column: "TemplateId",
                principalTable: "MyStoryTemplateItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryItems_MyStoryTemplateItems_TemplateId",
                table: "StoryItems");

            migrationBuilder.DropIndex(
                name: "IX_StoryItems_TemplateId",
                table: "StoryItems");

            migrationBuilder.RenameColumn(
                name: "TemplateId",
                table: "StoryItems",
                newName: "Content");

            migrationBuilder.CreateTable(
                name: "StoryPollItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Explanation = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryPollId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryPollItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryPollItem_StoryItems_StoryPollId",
                        column: x => x.StoryPollId,
                        principalTable: "StoryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryPollItem_StoryPollId",
                table: "StoryPollItem",
                column: "StoryPollId");
        }
    }
}
