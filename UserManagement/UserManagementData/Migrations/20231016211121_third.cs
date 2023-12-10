using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementData.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] {  "Id", "Name" },
                values: new object[] { new Guid("43a4f009-17a1-42e4-aa98-60ec74f99dd4"), "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("43a4f009-17a1-42e4-aa98-60ec74f99dd4"));
        }
    }
}
