using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class studentsdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "CreatedAt", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 21, new DateTime(2025, 12, 24, 1, 46, 13, 807, DateTimeKind.Local).AddTicks(3696), "nin.ramishvili4@gmail.com", "Nino", "Ramishvili" },
                    { 2, 22, new DateTime(2025, 12, 24, 1, 46, 13, 807, DateTimeKind.Local).AddTicks(3767), "1234@mail.com", "Jane", "Smith" },
                    { 4, 19, new DateTime(2025, 12, 24, 1, 46, 13, 807, DateTimeKind.Local).AddTicks(3770), "abc@gmail.com", "Alice", "ramishvili" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
