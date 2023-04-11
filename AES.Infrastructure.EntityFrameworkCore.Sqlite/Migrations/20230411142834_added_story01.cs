using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class added_story01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyStoryTemplateItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    MyStoryTemplateId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MyStoryTemplateItemType = table.Column<int>(type: "INTEGER", nullable: false),
                    Bits = table.Column<byte[]>(type: "BLOB", nullable: true),
                    ContentType = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyStoryTemplateItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyStoryTemplateItems_MyStoryTemplates_MyStoryTemplateId",
                        column: x => x.MyStoryTemplateId,
                        principalTable: "MyStoryTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyStoryTemplateItems_MyStoryTemplateId",
                table: "MyStoryTemplateItems",
                column: "MyStoryTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyStoryTemplateItems");
        }
    }
}
