using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagementData.Migrations
{
    /// <inheritdoc />
    public partial class changedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6efba3bf-7b23-445c-9f90-f72f19f3119b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e11ab0d3-2865-4482-8fc7-ddcd906f9dc9"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("3b76541c-d83b-44be-83f3-06b2116e39c8"), "floreaneusebiu@gmail.com", "Eusebiu", "pass", "Admin" },
                    { new Guid("e407eab8-0f3e-43d0-a8b8-b90d724ce8ab"), "razvanb@yahoo.com", "Razvan", "pass", "Client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3b76541c-d83b-44be-83f3-06b2116e39c8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e407eab8-0f3e-43d0-a8b8-b90d724ce8ab"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Username");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
