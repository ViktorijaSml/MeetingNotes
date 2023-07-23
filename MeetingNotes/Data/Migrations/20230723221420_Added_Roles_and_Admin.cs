using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_Roles_and_Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fddf8f33-b7de-4510-b57f-c28635c0a502", "AQAAAAIAAYagAAAAEDAg6BkLP4mBcvbn71wTDgecd38sb408W2sGXYIvS5cRY6sHJZBvHGNvtfxSKZX8og==", "a3d35204-0b7c-4e44-b786-6c1e312d859b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4736b7b-4dcf-be6b-8b03-e299b4836146", "AQAAAAEAACcQAAAAEIB/N9AG5QrJ4XU3szWuwqgqG7qQ8CMr9dzz3f9F1lB84j0CxarXMAvnA6i0Exj/7Q==", "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3A" });
        }
    }
}
