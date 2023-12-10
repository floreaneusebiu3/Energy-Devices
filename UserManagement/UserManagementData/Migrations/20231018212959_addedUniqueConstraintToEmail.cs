using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagementData.Migrations
{
    /// <inheritdoc />
    public partial class addedUniqueConstraintToEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3b76541c-d83b-44be-83f3-06b2116e39c8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e407eab8-0f3e-43d0-a8b8-b90d724ce8ab"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("7fd0b951-6dde-44c7-a9ae-27d59e199b32"), "razvanb@yahoo.com", "Razvan", "pass", "Client" },
                    { new Guid("d81aca9f-4dd3-4750-8e3a-3d7df37f5442"), "floreaneusebiu@gmail.com", "Eusebiu", "pass", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7fd0b951-6dde-44c7-a9ae-27d59e199b32"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d81aca9f-4dd3-4750-8e3a-3d7df37f5442"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("3b76541c-d83b-44be-83f3-06b2116e39c8"), "floreaneusebiu@gmail.com", "Eusebiu", "pass", "Admin" },
                    { new Guid("e407eab8-0f3e-43d0-a8b8-b90d724ce8ab"), "razvanb@yahoo.com", "Razvan", "pass", "Client" }
                });
        }
    }
}
