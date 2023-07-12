using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Worker_IsManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Manager");

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "Worker",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Worker");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Note",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Manager",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Manager",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
