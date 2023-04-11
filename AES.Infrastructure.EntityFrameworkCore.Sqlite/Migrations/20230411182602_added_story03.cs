using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class added_story03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoryTemplateId",
                table: "ModuleItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleItems_StoryTemplateId",
                table: "ModuleItems",
                column: "StoryTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_MyStoryTemplates_StoryTemplateId",
                table: "ModuleItems",
                column: "StoryTemplateId",
                principalTable: "MyStoryTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_MyStoryTemplates_StoryTemplateId",
                table: "ModuleItems");

            migrationBuilder.DropIndex(
                name: "IX_ModuleItems_StoryTemplateId",
                table: "ModuleItems");

            migrationBuilder.DropColumn(
                name: "StoryTemplateId",
                table: "ModuleItems");
        }
    }
}
