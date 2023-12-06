using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateForSansTopuGetRandomLogs : Migration
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
                name: "SansTopuGetRandomLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandomPlusNumber = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SansTopuGetRandomLogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22cf17c1-7c80-465e-a808-949fd2e6c052", null, "User", "USER" },
                    { "2c2fb5aa-46f8-4bc2-ba2b-53401c4db8ea", null, "Admin", "ADMIN" },
                    { "65d14bfc-6761-4363-bcba-1e0f878e59a3", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SansTopuGetRandomLogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22cf17c1-7c80-465e-a808-949fd2e6c052");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c2fb5aa-46f8-4bc2-ba2b-53401c4db8ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65d14bfc-6761-4363-bcba-1e0f878e59a3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60f30142-4609-486c-869a-8ef4d733689f", null, "Admin", "ADMIN" },
                    { "aab52cd7-f34d-4466-93a4-d9a9ee103473", null, "User", "USER" },
                    { "b3d7e4b8-357b-4fb7-b6c8-21b1b33a4afe", null, "Editor", "EDITOR" }
                });
        }
    }
}
