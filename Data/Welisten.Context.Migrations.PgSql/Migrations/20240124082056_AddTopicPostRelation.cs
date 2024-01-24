using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicPostRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostTopic",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "integer", nullable: false),
                    TopicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTopic", x => new { x.PostsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_PostTopic_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTopic_TopicsId",
                table: "PostTopic",
                column: "TopicsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTopic");
        }
    }
}
