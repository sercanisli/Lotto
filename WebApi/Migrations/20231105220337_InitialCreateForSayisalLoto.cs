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
                keyValue: "4cec89ea-79a4-4932-bcd6-babea96ba36f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56f6db03-b602-46f5-89f7-abc2e199b380");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a2a880d-9f66-478e-b4f3-166ae54e09a5");

            migrationBuilder.CreateTable(
                name: "SayisalLotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SayisalLotos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "149dd7fb-8a7b-40ac-9180-96c12a4f4078", null, "Admin", "ADMIN" },
                    { "85507ffa-f696-4c72-851e-4dedd9584ca9", null, "User", "USER" },
                    { "a4f72667-160e-4d67-b524-61118f69b7ca", null, "Editor", "EDITOR" }
                });

            migrationBuilder.InsertData(
                table: "SayisalLotos",
                columns: new[] { "Id", "Date", "Numbers" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "6,25,17,13,27,60" },
                    { 2, new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "19,17,9,23,27,45" },
                    { 3, new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "13,45,53,52,27,3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SayisalLotos");

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
                    { "4cec89ea-79a4-4932-bcd6-babea96ba36f", null, "Admin", "ADMIN" },
                    { "56f6db03-b602-46f5-89f7-abc2e199b380", null, "Editor", "EDITOR" },
                    { "6a2a880d-9f66-478e-b4f3-166ae54e09a5", null, "User", "USER" }
                });
        }
    }
}
