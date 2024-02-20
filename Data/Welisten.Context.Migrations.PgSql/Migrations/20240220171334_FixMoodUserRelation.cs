using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class FixMoodUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodRecordEvents_Events_EventId",
                table: "MoodRecordEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Moods_Users_UserId",
                table: "Moods");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Moods_UserId",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Moods");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodRecordEvents_EventTypes_EventId",
                table: "MoodRecordEvents",
                column: "EventId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodRecordEvents_EventTypes_EventId",
                table: "MoodRecordEvents");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Moods",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventTypeId = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moods_UserId",
                table: "Moods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Uid",
                table: "Events",
                column: "Uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoodRecordEvents_Events_EventId",
                table: "MoodRecordEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moods_Users_UserId",
                table: "Moods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
