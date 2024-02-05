using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguringTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTopic_Posts_PostsId",
                table: "PostTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTopic_Topics_TopicsId",
                table: "PostTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTopic",
                table: "PostTopic");

            migrationBuilder.DropColumn(
                name: "TopicIds",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "PostTopic",
                newName: "PostsTopics");

            migrationBuilder.RenameIndex(
                name: "IX_PostTopic_TopicsId",
                table: "PostsTopics",
                newName: "IX_PostsTopics_TopicsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsTopics",
                table: "PostsTopics",
                columns: new[] { "PostsId", "TopicsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTopics_Posts_PostsId",
                table: "PostsTopics",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTopics_Topics_TopicsId",
                table: "PostsTopics",
                column: "TopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsTopics_Posts_PostsId",
                table: "PostsTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsTopics_Topics_TopicsId",
                table: "PostsTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsTopics",
                table: "PostsTopics");

            migrationBuilder.RenameTable(
                name: "PostsTopics",
                newName: "PostTopic");

            migrationBuilder.RenameIndex(
                name: "IX_PostsTopics_TopicsId",
                table: "PostTopic",
                newName: "IX_PostTopic_TopicsId");

            migrationBuilder.AddColumn<Guid[]>(
                name: "TopicIds",
                table: "Posts",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTopic",
                table: "PostTopic",
                columns: new[] { "PostsId", "TopicsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTopic_Posts_PostsId",
                table: "PostTopic",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTopic_Topics_TopicsId",
                table: "PostTopic",
                column: "TopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
