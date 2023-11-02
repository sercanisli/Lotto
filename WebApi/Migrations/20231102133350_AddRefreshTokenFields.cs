using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5438a908-b6d0-43e9-8196-c9a6fc23ec9c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5acee7b1-cece-4707-9da6-0025c5d90f16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0bd6de8-017f-4e21-8422-ca509cc755f5");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5438a908-b6d0-43e9-8196-c9a6fc23ec9c", null, "Admin", "ADMIN" },
                    { "5acee7b1-cece-4707-9da6-0025c5d90f16", null, "User", "USER" },
                    { "b0bd6de8-017f-4e21-8422-ca509cc755f5", null, "Editor", "EDITOR" }
                });
        }
    }
}
