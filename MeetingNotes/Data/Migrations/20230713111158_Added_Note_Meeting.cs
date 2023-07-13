using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Note_Meeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Meeting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_NoteId",
                table: "Meeting",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Note_NoteId",
                table: "Meeting",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Note_NoteId",
                table: "Meeting");

            migrationBuilder.DropIndex(
                name: "IX_Meeting_NoteId",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Meeting");
        }
    }
}
