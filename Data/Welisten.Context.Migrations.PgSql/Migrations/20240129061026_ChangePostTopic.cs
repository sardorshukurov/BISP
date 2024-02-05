using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class ChangePostTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "TopicIds",
                table: "Posts",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicIds",
                table: "Posts");
        }
    }
}
