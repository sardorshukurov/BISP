using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddPostConfugration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostCounts_Posts_PostId",
                table: "PostCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_Posts_PostsId",
                table: "PostReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReaction_Reactions_ReactionsId",
                table: "PostReaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCounts",
                table: "PostCounts");

            migrationBuilder.DropIndex(
                name: "IX_PostCounts_PostId",
                table: "PostCounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostCounts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "posts");

            migrationBuilder.RenameTable(
                name: "PostCounts",
                newName: "postcounts");

            migrationBuilder.RenameTable(
                name: "PostReaction",
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
                name: "IX_PostReaction_ReactionsId",
                table: "posts_reactions",
                newName: "IX_posts_reactions_ReactionsId");

            migrationBuilder.AddColumn<int>(
                name: "PostCountId",
                table: "posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "postcounts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            migrationBuilder.CreateIndex(
                name: "IX_posts_PostCountId",
                table: "posts",
                column: "PostCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_postcounts_posts_Id",
                table: "postcounts",
                column: "Id",
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
                name: "FK_posts_postcounts_PostCountId",
                table: "posts",
                column: "PostCountId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_postcounts_posts_Id",
                table: "postcounts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_Users_UserId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_postcounts_PostCountId",
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

            migrationBuilder.DropIndex(
                name: "IX_posts_PostCountId",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_postcounts",
                table: "postcounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts_reactions",
                table: "posts_reactions");

            migrationBuilder.DropColumn(
                name: "PostCountId",
                table: "posts");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "postcounts",
                newName: "PostCounts");

            migrationBuilder.RenameTable(
                name: "posts_reactions",
                newName: "PostReaction");

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
                table: "PostReaction",
                newName: "IX_PostReaction_ReactionsId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PostCounts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "PostCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCounts",
                table: "PostCounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction",
                columns: new[] { "PostsId", "ReactionsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostCounts_PostId",
                table: "PostCounts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostCounts_Posts_PostId",
                table: "PostCounts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_Posts_PostsId",
                table: "PostReaction",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReaction_Reactions_ReactionsId",
                table: "PostReaction",
                column: "ReactionsId",
                principalTable: "Reactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
