using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_Users_UserId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_postcounts_Id",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_reactions_Reactions_ReactionsId",
                table: "posts_reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_reactions_posts_PostsId",
                table: "posts_reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_postcounts",
                table: "postcounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts_reactions",
                table: "posts_reactions");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "postcounts",
                newName: "PostCounts");

            migrationBuilder.RenameTable(
                name: "posts_reactions",
                newName: "PostsReactions");

            migrationBuilder.RenameIndex(
                name: "IX_posts_UserId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_posts_Uid",
                table: "Posts",
                newName: "IX_Posts_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_posts_reactions_ReactionsId",
                table: "PostsReactions",
                newName: "IX_PostsReactions_ReactionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCounts",
                table: "PostCounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsReactions",
                table: "PostsReactions",
                columns: new[] { "PostsId", "ReactionsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCounts_Id",
                table: "Posts",
                column: "Id",
                principalTable: "PostCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsReactions_Posts_PostsId",
                table: "PostsReactions",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsReactions_Reactions_ReactionsId",
                table: "PostsReactions",
                column: "ReactionsId",
                principalTable: "Reactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCounts_Id",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsReactions_Posts_PostsId",
                table: "PostsReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsReactions_Reactions_ReactionsId",
                table: "PostsReactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCounts",
                table: "PostCounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsReactions",
                table: "PostsReactions");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "posts");

            migrationBuilder.RenameTable(
                name: "PostCounts",
                newName: "postcounts");

            migrationBuilder.RenameTable(
                name: "PostsReactions",
                newName: "posts_reactions");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "posts",
                newName: "IX_posts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_Uid",
                table: "posts",
                newName: "IX_posts_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_PostsReactions_ReactionsId",
                table: "posts_reactions",
                newName: "IX_posts_reactions_ReactionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts",
                table: "posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_postcounts",
                table: "postcounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts_reactions",
                table: "posts_reactions",
                columns: new[] { "PostsId", "ReactionsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_Users_UserId",
                table: "posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_postcounts_Id",
                table: "posts",
                column: "Id",
                principalTable: "postcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_reactions_Reactions_ReactionsId",
                table: "posts_reactions",
                column: "ReactionsId",
                principalTable: "Reactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_reactions_posts_PostsId",
                table: "posts_reactions",
                column: "PostsId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
