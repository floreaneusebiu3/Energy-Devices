using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagementData.Migrations
{
    /// <inheritdoc />
    public partial class addedSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { new Guid("2f6cd671-f51e-4dcb-a20c-9fd2389c4def"), "Admin" },
                    { new Guid("3889a663-2446-4027-8911-3c21eff653cb"), "Client" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("6efba3bf-7b23-445c-9f90-f72f19f3119b"), "Eusebiu", "pass", new Guid("2f6cd671-f51e-4dcb-a20c-9fd2389c4def"), "floreaneusebiu" },
                    { new Guid("e11ab0d3-2865-4482-8fc7-ddcd906f9dc9"), "Razvan", "pass", new Guid("3889a663-2446-4027-8911-3c21eff653cb"), "razvanb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6efba3bf-7b23-445c-9f90-f72f19f3119b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e11ab0d3-2865-4482-8fc7-ddcd906f9dc9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2f6cd671-f51e-4dcb-a20c-9fd2389c4def"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3889a663-2446-4027-8911-3c21eff653cb"));

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
    }
}
