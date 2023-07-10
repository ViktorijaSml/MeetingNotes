using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_All_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Worker");

            migrationBuilder.AddColumn<DateTime>(
                name: "HiringDate",
                table: "Worker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Worker",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Worker",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    MeetingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    NotesId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.MeetingId);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worker_IdentityUserId",
                table: "Worker",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_ManagerId",
                table: "Worker",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_AspNetUsers_IdentityUserId",
                table: "Worker",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Manager_ManagerId",
                table: "Worker",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_AspNetUsers_IdentityUserId",
                table: "Worker");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Manager_ManagerId",
                table: "Worker");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Worker_IdentityUserId",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_ManagerId",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "HiringDate",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Worker");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
