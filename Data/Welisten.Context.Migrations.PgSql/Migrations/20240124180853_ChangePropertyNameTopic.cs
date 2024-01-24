using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyNameTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Topics");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Topics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Topics");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Topics",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
