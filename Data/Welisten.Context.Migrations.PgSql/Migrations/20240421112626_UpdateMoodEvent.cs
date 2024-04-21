using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Welisten.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoodEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoodRecordEvents");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "MoodRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MoodRecords_EventId",
                table: "MoodRecords",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodRecords_EventTypes_EventId",
                table: "MoodRecords",
                column: "EventId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodRecords_EventTypes_EventId",
                table: "MoodRecords");

            migrationBuilder.DropIndex(
                name: "IX_MoodRecords_EventId",
                table: "MoodRecords");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "MoodRecords");

            migrationBuilder.CreateTable(
                name: "MoodRecordEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    MoodRecordId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodRecordEvents", x => new { x.EventId, x.MoodRecordId });
                    table.ForeignKey(
                        name: "FK_MoodRecordEvents_EventTypes_EventId",
                        column: x => x.EventId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoodRecordEvents_MoodRecords_MoodRecordId",
                        column: x => x.MoodRecordId,
                        principalTable: "MoodRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoodRecordEvents_MoodRecordId",
                table: "MoodRecordEvents",
                column: "MoodRecordId");
        }
    }
}
