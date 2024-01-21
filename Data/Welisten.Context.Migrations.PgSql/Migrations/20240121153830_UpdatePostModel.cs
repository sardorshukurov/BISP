using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postcounts_posts_Id",
                table: "postcounts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_postcounts_PostCountId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_PostCountId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "PostCountId",
                table: "posts");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "posts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "postcounts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_postcounts_Id",
                table: "posts",
                column: "Id",
                principalTable: "postcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_postcounts_Id",
                table: "posts");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "posts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            migrationBuilder.CreateIndex(
                name: "IX_posts_PostCountId",
                table: "posts",
                column: "PostCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_postcounts_posts_Id",
                table: "postcounts",
                column: "Id",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_postcounts_PostCountId",
                table: "posts",
                column: "PostCountId",
                principalTable: "postcounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
