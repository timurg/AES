using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class mystory_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyStoryTemplateQuizItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Explanation = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    MyStoryTemplateQuizId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyStoryTemplateQuizItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyStoryTemplateQuizItem_MyStoryTemplateItems_MyStoryTemplateQuizId",
                        column: x => x.MyStoryTemplateQuizId,
                        principalTable: "MyStoryTemplateItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyStoryTemplateQuizItem_MyStoryTemplateQuizId",
                table: "MyStoryTemplateQuizItem",
                column: "MyStoryTemplateQuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyStoryTemplateQuizItem");
        }
    }
}
