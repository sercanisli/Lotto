using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "149dd7fb-8a7b-40ac-9180-96c12a4f4078");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85507ffa-f696-4c72-851e-4dedd9584ca9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4f72667-160e-4d67-b524-61118f69b7ca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b1262b0-4c05-4a8b-9d21-3c403038a867", null, "User", "USER" },
                    { "4d2b1712-0bf5-4263-ad02-d8316b764e21", null, "Editor", "EDITOR" },
                    { "df473bc9-7729-44d9-92f3-58327516967e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b1262b0-4c05-4a8b-9d21-3c403038a867");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d2b1712-0bf5-4263-ad02-d8316b764e21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df473bc9-7729-44d9-92f3-58327516967e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "149dd7fb-8a7b-40ac-9180-96c12a4f4078", null, "Admin", "ADMIN" },
                    { "85507ffa-f696-4c72-851e-4dedd9584ca9", null, "User", "USER" },
                    { "a4f72667-160e-4d67-b524-61118f69b7ca", null, "Editor", "EDITOR" }
                });
        }
    }
}
