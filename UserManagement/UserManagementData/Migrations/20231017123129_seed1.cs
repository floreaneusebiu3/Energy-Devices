using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagementData.Migrations
{
    /// <inheritdoc />
    public partial class seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { new Guid("390c0c32-c3f9-425d-b2d3-ad4ba9715011"), "Client" },
                    { new Guid("eff669c8-a309-46f1-be7b-6e0fc4494826"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "RoleId", "Username" },
                values: new object[] { new Guid("7d40d488-949b-4ab3-9e23-d91a58524289"), "Eusebiu", "pass", new Guid("eff669c8-a309-46f1-be7b-6e0fc4494826"), "floreaneusebiu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("390c0c32-c3f9-425d-b2d3-ad4ba9715011"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7d40d488-949b-4ab3-9e23-d91a58524289"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eff669c8-a309-46f1-be7b-6e0fc4494826"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("f99428f5-221e-40e8-9ac3-2ab09fd9b5d3"), "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "RoleId", "Username" },
                values: new object[] { new Guid("d542bcbd-fa0d-4f92-b6a1-579296f447ae"), "Eusebiu", "pass", new Guid("f99428f5-221e-40e8-9ac3-2ab09fd9b5d3"), "floreaneusebiu" });
        }
    }
}
