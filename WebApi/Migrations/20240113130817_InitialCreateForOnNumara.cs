using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForOnNumara : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf1ee80-3995-40ee-a593-1681b1d267cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ace94cc0-c158-499d-9883-1f7ad6f2f2d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3c28195-18dc-415e-89db-48ad6600c2bd");

            migrationBuilder.DeleteData(
                table: "OnNumaras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OnNumaras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OnNumaras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 1, 13, 16, 8, 16, 488, DateTimeKind.Local).AddTicks(6246));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 1, 13, 16, 8, 16, 488, DateTimeKind.Local).AddTicks(6253));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62ff7d8c-a7a9-4474-85fb-45cea82d903c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92c651c2-5cc4-4783-834d-4235f74d6448");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbc4b342-0f3f-4d9c-ae7c-cf8c170d089c");

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 31, 1, 59, 49, 260, DateTimeKind.Local).AddTicks(6608));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 31, 1, 59, 49, 260, DateTimeKind.Local).AddTicks(6616));
        }
    }
}
