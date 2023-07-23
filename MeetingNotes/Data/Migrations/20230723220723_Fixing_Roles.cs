using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeetingNotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixing_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "012345f0-akl2–42de-afbf-59ccfdaf72cf6", "2", "Worker", "WORKER" },
                    { "341743f0-a67k–42de-afbf-59asdfac72cf6", "3", "Manager", "MANAGER" },
                    { "74d04fa7-36b6-4fa2-ade4-d2a1759e4091", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22e40406-8a9d-2d82-912c-5d6a640ee696", 0, "c4736b7b-4dcf-be6b-8b03-e299b4836146", "me@example.com", true, false, null, "ME@EXAMPLE.COM", "ME@EXAMPLE.COM", "AQAAAAEAACcQAAAAEIB/N9AG5QrJ4XU3szWuwqgqG7qQ8CMr9dzz3f9F1lB84j0CxarXMAvnA6i0Exj/7Q==", null, false, "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3A", false, "me@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "74d04fa7-36b6-4fa2-ade4-d2a1759e4091", "22e40406-8a9d-2d82-912c-5d6a640ee696" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "012345f0-akl2–42de-afbf-59ccfdaf72cf6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "341743f0-a67k–42de-afbf-59asdfac72cf6");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "74d04fa7-36b6-4fa2-ade4-d2a1759e4091", "22e40406-8a9d-2d82-912c-5d6a640ee696" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74d04fa7-36b6-4fa2-ade4-d2a1759e4091");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696");
        }
    }
}
