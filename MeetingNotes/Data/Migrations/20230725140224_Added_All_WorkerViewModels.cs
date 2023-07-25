using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_All_WorkerViewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_AspNetUsers_identityUserId",
                table: "Worker");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Manager_ManagerId",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Worker_ManagerId",
                table: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Worker");

            migrationBuilder.AlterColumn<string>(
                name: "identityUserId",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Manager",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91e69ef6-0955-40db-bfa0-2299d874cc65", "AQAAAAIAAYagAAAAEMBmESDeUi3ZXXjJz1QbDc7/pIOcEZFqtJdDawXqWozHqrcblRTEEMq61ps0R3VIXQ==", "dfe5a351-099b-47ac-8110-e56346c98e2b" });

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

            migrationBuilder.AlterColumn<string>(
                name: "identityUserId",
                table: "Worker",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Worker",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Manager",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "ManagerId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fddf8f33-b7de-4510-b57f-c28635c0a502", "AQAAAAIAAYagAAAAEDAg6BkLP4mBcvbn71wTDgecd38sb408W2sGXYIvS5cRY6sHJZBvHGNvtfxSKZX8og==", "a3d35204-0b7c-4e44-b786-6c1e312d859b" });

            migrationBuilder.CreateIndex(
                name: "IX_Worker_ManagerId",
                table: "Worker",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_AspNetUsers_identityUserId",
                table: "Worker",
                column: "identityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Manager_ManagerId",
                table: "Worker",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "ManagerId");
        }
    }
}
