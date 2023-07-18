using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Worker_identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_AspNetUsers_IdentityUserId",
                table: "Worker");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Worker",
                newName: "identityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_IdentityUserId",
                table: "Worker",
                newName: "IX_Worker_identityUserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "identityUserId",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_AspNetUsers_identityUserId",
                table: "Worker",
                column: "identityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_AspNetUsers_identityUserId",
                table: "Worker");

            migrationBuilder.RenameColumn(
                name: "identityUserId",
                table: "Worker",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_identityUserId",
                table: "Worker",
                newName: "IX_Worker_IdentityUserId");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_AspNetUsers_IdentityUserId",
                table: "Worker",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
