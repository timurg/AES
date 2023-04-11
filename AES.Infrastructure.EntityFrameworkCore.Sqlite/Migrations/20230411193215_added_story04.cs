using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class added_story04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_MyStoryTemplates_StoryTemplateId",
                table: "ModuleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryItems_ModuleItems_MyStoryId",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "StoryStep",
                table: "ModuleItems");

            migrationBuilder.RenameColumn(
                name: "StoryTemplateId",
                table: "ModuleItems",
                newName: "LearningProcessId");

            migrationBuilder.RenameColumn(
                name: "ModuleItemType",
                table: "ModuleItems",
                newName: "LearningProcessesType");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleItems_StoryTemplateId",
                table: "ModuleItems",
                newName: "IX_ModuleItems_LearningProcessId");

            migrationBuilder.CreateTable(
                name: "LearningProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LearningProcessesType = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryStep = table.Column<int>(type: "INTEGER", nullable: true),
                    StoryTemplateId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningProcesses_MyStoryTemplates_StoryTemplateId",
                        column: x => x.StoryTemplateId,
                        principalTable: "MyStoryTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningProcesses_StoryTemplateId",
                table: "LearningProcesses",
                column: "StoryTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_LearningProcesses_LearningProcessId",
                table: "ModuleItems",
                column: "LearningProcessId",
                principalTable: "LearningProcesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryItems_LearningProcesses_MyStoryId",
                table: "StoryItems",
                column: "MyStoryId",
                principalTable: "LearningProcesses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_LearningProcesses_LearningProcessId",
                table: "ModuleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryItems_LearningProcesses_MyStoryId",
                table: "StoryItems");

            migrationBuilder.DropTable(
                name: "LearningProcesses");

            migrationBuilder.RenameColumn(
                name: "LearningProcessesType",
                table: "ModuleItems",
                newName: "ModuleItemType");

            migrationBuilder.RenameColumn(
                name: "LearningProcessId",
                table: "ModuleItems",
                newName: "StoryTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleItems_LearningProcessId",
                table: "ModuleItems",
                newName: "IX_ModuleItems_StoryTemplateId");

            migrationBuilder.AddColumn<int>(
                name: "StoryStep",
                table: "ModuleItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_MyStoryTemplates_StoryTemplateId",
                table: "ModuleItems",
                column: "StoryTemplateId",
                principalTable: "MyStoryTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryItems_ModuleItems_MyStoryId",
                table: "StoryItems",
                column: "MyStoryId",
                principalTable: "ModuleItems",
                principalColumn: "Id");
        }
    }
}
