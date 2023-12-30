using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSuperLotoLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "049e33ac-4cf5-4e24-ae8d-c51ec084d752");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d5122ba-3f89-454a-bda8-102327405347");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd38005d-c8b5-4824-ba3a-213ccfe57e53");

            migrationBuilder.CreateTable(
                name: "SuperLotoLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperLotoLogs", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperLotoLogs");

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

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 30, 16, 27, 21, 131, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 30, 16, 27, 21, 131, DateTimeKind.Local).AddTicks(7993));
        }
    }
}
