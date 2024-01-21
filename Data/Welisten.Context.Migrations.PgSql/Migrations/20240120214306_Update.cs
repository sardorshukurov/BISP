using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicUser_topics_TopicsId",
                table: "TopicUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicUser_users_UsersId",
                table: "TopicUser");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_PostId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_posts_PostId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_moods_users_UserId",
                table: "moods");

            migrationBuilder.DropForeignKey(
                name: "FK_moodtypes_moods_Id",
                table: "moodtypes");

            migrationBuilder.DropForeignKey(
                name: "FK_postcounts_posts_Id",
                table: "postcounts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_reactions_posts_PostsId",
                table: "posts_reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_reactions_reactions_ReactionsId",
                table: "posts_reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_claims_users_UserId",
                table: "user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_logins_users_UserId",
                table: "user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_claims_user_roles_RoleId",
                table: "user_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_owners_user_roles_RoleId",
                table: "user_role_owners");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_owners_users_UserId",
                table: "user_role_owners");

            migrationBuilder.DropForeignKey(
                name: "FK_user_tokens_users_UserId",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_topics",
                table: "topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reactions",
                table: "reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_postcounts",
                table: "postcounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_moodtypes",
                table: "moodtypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_moods",
                table: "moods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comments",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role_owners",
                table: "user_role_owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role_claims",
                table: "user_role_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts_reactions",
                table: "posts_reactions");

            migrationBuilder.RenameTable(
                name: "topics",
                newName: "Topics");

            migrationBuilder.RenameTable(
                name: "reactions",
                newName: "Reactions");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "postcounts",
                newName: "PostCounts");

            migrationBuilder.RenameTable(
                name: "moodtypes",
                newName: "MoodTypes");

            migrationBuilder.RenameTable(
                name: "moods",
                newName: "Moods");

            migrationBuilder.RenameTable(
                name: "comments",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "user_role_owners",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "user_role_claims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "user_logins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "user_claims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "posts_reactions",
                newName: "PostReaction");

            migrationBuilder.RenameIndex(
                name: "IX_topics_Uid",
                table: "Topics",
                newName: "IX_Topics_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_reactions_Uid",
                table: "Reactions",
                newName: "IX_Reactions_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_posts_Uid",
                table: "Posts",
                newName: "IX_Posts_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_moodtypes_Uid",
                table: "MoodTypes",
                newName: "IX_MoodTypes_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_moods_UserId",
                table: "Moods",
                newName: "IX_Moods_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_moods_Uid",
                table: "Moods",
                newName: "IX_Moods_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_comments_Uid",
                table: "Comments",
                newName: "IX_Comments_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_comments_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_owners_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_claims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_user_logins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_claims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MoodTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "MoodId",
                table: "MoodTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCounts",
                table: "PostCounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoodTypes",
                table: "MoodTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moods",
                table: "Moods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction",
                columns: new[] { "PostsId", "ReactionsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostCounts_PostId",
                table: "PostCounts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_MoodTypes_MoodId",
                table: "MoodTypes",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoodTypes_Moods_MoodId",
                table: "MoodTypes",
                column: "MoodId",
                principalTable: "Moods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moods_AspNetUsers_UserId",
                table: "Moods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
                name: "FK_TopicUser_AspNetUsers_UsersId",
                table: "TopicUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUser_Topics_TopicsId",
                table: "TopicUser",
                column: "TopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_MoodTypes_Moods_MoodId",
                table: "MoodTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Moods_AspNetUsers_UserId",
                table: "Moods");

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
                name: "FK_TopicUser_AspNetUsers_UsersId",
                table: "TopicUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicUser_Topics_TopicsId",
                table: "TopicUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions");

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
                name: "PK_Moods",
                table: "Moods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoodTypes",
                table: "MoodTypes");

            migrationBuilder.DropIndex(
                name: "IX_MoodTypes_MoodId",
                table: "MoodTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostReaction",
                table: "PostReaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostCounts");

            migrationBuilder.DropColumn(
                name: "MoodId",
                table: "MoodTypes");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Topics",
                newName: "topics");

            migrationBuilder.RenameTable(
                name: "Reactions",
                newName: "reactions");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "posts");

            migrationBuilder.RenameTable(
                name: "PostCounts",
                newName: "postcounts");

            migrationBuilder.RenameTable(
                name: "Moods",
                newName: "moods");

            migrationBuilder.RenameTable(
                name: "MoodTypes",
                newName: "moodtypes");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "comments");

            migrationBuilder.RenameTable(
                name: "PostReaction",
                newName: "posts_reactions");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "user_tokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "user_role_owners");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "user_logins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "user_claims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "user_role_claims");

            migrationBuilder.RenameIndex(
                name: "IX_Topics_Uid",
                table: "topics",
                newName: "IX_topics_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Reactions_Uid",
                table: "reactions",
                newName: "IX_reactions_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_Uid",
                table: "posts",
                newName: "IX_posts_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Moods_UserId",
                table: "moods",
                newName: "IX_moods_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Moods_Uid",
                table: "moods",
                newName: "IX_moods_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_MoodTypes_Uid",
                table: "moodtypes",
                newName: "IX_moodtypes_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_Uid",
                table: "comments",
                newName: "IX_comments_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "comments",
                newName: "IX_comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostReaction_ReactionsId",
                table: "posts_reactions",
                newName: "IX_posts_reactions_ReactionsId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "user_role_owners",
                newName: "IX_user_role_owners_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "user_logins",
                newName: "IX_user_logins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "user_claims",
                newName: "IX_user_claims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "user_role_claims",
                newName: "IX_user_role_claims_RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "postcounts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "moodtypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_topics",
                table: "topics",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reactions",
                table: "reactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts",
                table: "posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_postcounts",
                table: "postcounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_moods",
                table: "moods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_moodtypes",
                table: "moodtypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comments",
                table: "comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts_reactions",
                table: "posts_reactions",
                columns: new[] { "PostsId", "ReactionsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role_owners",
                table: "user_role_owners",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role_claims",
                table: "user_role_claims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUser_topics_TopicsId",
                table: "TopicUser",
                column: "TopicsId",
                principalTable: "topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUser_users_UsersId",
                table: "TopicUser",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_PostId",
                table: "comments",
                column: "PostId",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_posts_PostId",
                table: "comments",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_moods_users_UserId",
                table: "moods",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_moodtypes_moods_Id",
                table: "moodtypes",
                column: "Id",
                principalTable: "moods",
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
                name: "FK_posts_reactions_posts_PostsId",
                table: "posts_reactions",
                column: "PostsId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_reactions_reactions_ReactionsId",
                table: "posts_reactions",
                column: "ReactionsId",
                principalTable: "reactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_claims_users_UserId",
                table: "user_claims",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_logins_users_UserId",
                table: "user_logins",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_claims_user_roles_RoleId",
                table: "user_role_claims",
                column: "RoleId",
                principalTable: "user_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_owners_user_roles_RoleId",
                table: "user_role_owners",
                column: "RoleId",
                principalTable: "user_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_owners_users_UserId",
                table: "user_role_owners",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_tokens_users_UserId",
                table: "user_tokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
