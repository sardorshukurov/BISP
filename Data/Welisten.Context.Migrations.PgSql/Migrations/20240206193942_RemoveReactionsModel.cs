using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReactionsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostsReactions");

            migrationBuilder.DropTable(
                name: "Reactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostsReactions",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "integer", nullable: false),
                    ReactionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsReactions", x => new { x.PostsId, x.ReactionsId });
                    table.ForeignKey(
                        name: "FK_PostsReactions_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsReactions_Reactions_ReactionsId",
                        column: x => x.ReactionsId,
                        principalTable: "Reactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostsReactions_ReactionsId",
                table: "PostsReactions",
                column: "ReactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_Uid",
                table: "Reactions",
                column: "Uid",
                unique: true);
        }
    }
}
