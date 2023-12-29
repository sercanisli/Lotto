using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSayisalLoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d367196-29ca-490b-96f6-20efdbeb7a1d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "949ba490-d365-4bfb-b601-b0183aa571d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5ed4667-5742-476c-bf84-680319e27389");

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 29, 18, 50, 22, 459, DateTimeKind.Local).AddTicks(7952));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 29, 18, 50, 22, 459, DateTimeKind.Local).AddTicks(7959));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7362e1f-ac2a-45ff-909f-e9c266a2976b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f75983e3-d3b3-45c7-8deb-1718ab724fbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f98dfe0a-2188-4073-a8e0-62dfd9ce835c");

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 29, 18, 2, 1, 336, DateTimeKind.Local).AddTicks(2479));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 29, 18, 2, 1, 336, DateTimeKind.Local).AddTicks(2487));
        }
    }
}
