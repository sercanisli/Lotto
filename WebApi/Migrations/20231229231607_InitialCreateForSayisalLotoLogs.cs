using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSayisalLotoLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "SayisalLotoLogs",
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
                    table.PrimaryKey("PK_SayisalLotoLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 30, 2, 16, 7, 395, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "SansTopuLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 30, 2, 16, 7, 395, DateTimeKind.Local).AddTicks(7946));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SayisalLotoLogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3639e419-8060-494b-bec1-8293491437e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64313271-af2e-497b-98b7-5d04debf6149");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3e33264-86b7-41ed-9c8f-4f5040d0df9a");

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
    }
}
