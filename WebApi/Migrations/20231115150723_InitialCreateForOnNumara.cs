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
                keyValue: "4b1262b0-4c05-4a8b-9d21-3c403038a867");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d2b1712-0bf5-4263-ad02-d8316b764e21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df473bc9-7729-44d9-92f3-58327516967e");

            migrationBuilder.CreateTable(
                name: "OnNumaras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnNumaras", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a90bd21-b09d-4e71-a100-0cdf1a738810", null, "User", "USER" },
                    { "2cfbcf0e-7557-47d4-9eef-9007edc11b50", null, "Editor", "EDITOR" },
                    { "c5545121-3d85-481a-8003-e42c173c7873", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "OnNumaras",
                columns: new[] { "Id", "Date", "Numbers" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "6,25,17,13,27,60,63,66,71,78" },
                    { 2, new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "19,17,9,23,27,45,47,53,64,67" },
                    { 3, new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "13,45,53,52,27,3,34,37,59,78" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnNumaras");

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
    }
}
