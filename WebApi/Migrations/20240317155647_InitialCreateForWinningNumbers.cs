using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForWinningNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "146a0e7f-0917-4dba-8b1b-108d93a128c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c57899f3-bb53-464a-b248-df6fcceac275");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8fd0ca5-165e-4d31-9f52-f9536da09696");

            migrationBuilder.CreateTable(
                name: "WinningNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinningNumbers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 3, 17, 18, 56, 47, 303, DateTimeKind.Local).AddTicks(1177));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 3, 17, 18, 56, 47, 303, DateTimeKind.Local).AddTicks(1184));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WinningNumbers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "022fc3b8-0bb7-465e-80b2-00e82246ac18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a505c54e-40b9-4ce9-aa08-cb1df630090c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6b35f26-fba1-4749-bb16-ea45586562aa");

            
            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 1, 14, 17, 4, 2, 733, DateTimeKind.Local).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 1, 14, 17, 4, 2, 733, DateTimeKind.Local).AddTicks(7195));
        }
    }
}
