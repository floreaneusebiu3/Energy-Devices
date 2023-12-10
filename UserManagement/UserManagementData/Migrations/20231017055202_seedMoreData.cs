using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementData.Migrations
{
    /// <inheritdoc />
    public partial class seedMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("43a4f009-17a1-42e4-aa98-60ec74f99dd4"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("f99428f5-221e-40e8-9ac3-2ab09fd9b5d3"), "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "RoleId", "Username" },
                values: new object[] { new Guid("d542bcbd-fa0d-4f92-b6a1-579296f447ae"), "Eusebiu", "pass", new Guid("f99428f5-221e-40e8-9ac3-2ab09fd9b5d3"), "floreaneusebiu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d542bcbd-fa0d-4f92-b6a1-579296f447ae"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f99428f5-221e-40e8-9ac3-2ab09fd9b5d3"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("43a4f009-17a1-42e4-aa98-60ec74f99dd4"), "Admin" });
        }
    }
}
