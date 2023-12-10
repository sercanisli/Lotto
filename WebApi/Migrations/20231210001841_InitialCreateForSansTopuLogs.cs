using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSansTopuLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "SansTopuLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandomPlusNumber = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SansTopuLogs", x => x.Id);
                });

          

            migrationBuilder.InsertData(
                table: "SansTopuLogs",
                columns: new[] { "Id", "Date", "RandomNumbers", "RandomPlusNumber", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 10, 3, 18, 40, 245, DateTimeKind.Local).AddTicks(1073), "5,10,15,20,25", 11, "sercanisli" },
                    { 2, new DateTime(2023, 12, 10, 3, 18, 40, 245, DateTimeKind.Local).AddTicks(1084), "6,7,17,21,27,16", 3, "esinduru" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SansTopuLogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "737efeae-6b9d-414f-9f1b-ff31c80efa97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abd5c78f-a795-4d24-ba2a-792e2f522937");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8f56aa4-d4a5-48d8-832e-fa50c6fb78fe");

           
        }
    }
}
