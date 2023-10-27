using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuperLotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperLotos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SuperLotos",
                columns: new[] { "Id", "Date", "Numbers" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "45,26,17,6,27,60" },
                    { 2, new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "25,7,9,17,27,42" },
                    { 3, new DateTime(2023, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "12,4,17,6,27,60" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperLotos");
        }
    }
}
