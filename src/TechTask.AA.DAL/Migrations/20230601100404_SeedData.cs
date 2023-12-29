using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechTask.AA.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Flights",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1, "Moderator" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "CF-DE-2C-A5-18-8A-FB-7B-DD-06-91-C7-BE-F8-87-BA-BA-78-B7-09-AA-DD-E8-E8-C5-35-32-9D-57-51-E6-FE", 1, "moderator" },
                    { 2, "04-F8-99-6D-A7-63-B7-A9-69-B1-02-8E-E3-00-75-69-EA-F3-A6-35-48-6D-DA-B2-11-D5-12-C8-5B-9D-F8-FB", 2, "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Flights",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
