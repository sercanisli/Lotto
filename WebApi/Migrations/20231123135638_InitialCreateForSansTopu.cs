using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSansTopu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a90bd21-b09d-4e71-a100-0cdf1a738810");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2cfbcf0e-7557-47d4-9eef-9007edc11b50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5545121-3d85-481a-8003-e42c173c7873");

            migrationBuilder.CreateTable(
                name: "SansTopus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlusNumber = table.Column<int>(type: "int", nullable: false),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SansTopus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60f30142-4609-486c-869a-8ef4d733689f", null, "Admin", "ADMIN" },
                    { "aab52cd7-f34d-4466-93a4-d9a9ee103473", null, "User", "USER" },
                    { "b3d7e4b8-357b-4fb7-b6c8-21b1b33a4afe", null, "Editor", "EDITOR" }
                });

            migrationBuilder.InsertData(
                table: "SansTopus",
                columns: new[] { "Id", "Date", "Numbers", "PlusNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "6,25,17,13,27", 4 },
                    { 2, new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "19,17,9,5,34", 9 },
                    { 3, new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "13,12,1,27,22", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SansTopus");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60f30142-4609-486c-869a-8ef4d733689f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aab52cd7-f34d-4466-93a4-d9a9ee103473");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3d7e4b8-357b-4fb7-b6c8-21b1b33a4afe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a90bd21-b09d-4e71-a100-0cdf1a738810", null, "User", "USER" },
                    { "2cfbcf0e-7557-47d4-9eef-9007edc11b50", null, "Editor", "EDITOR" },
                    { "c5545121-3d85-481a-8003-e42c173c7873", null, "Admin", "ADMIN" }
                });
        }
    }
}
