using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class mystory_14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "StoryItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "StoryItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "StoryItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjectId",
                table: "StoryItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "SelectedItem",
                table: "StoryItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoryPollItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Explanation = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    Order = table.Column<uint>(type: "INTEGER", nullable: true),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryPollItem");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "StoryItems");

            migrationBuilder.DropColumn(
                name: "SelectedItem",
                table: "StoryItems");
        }
    }
}
