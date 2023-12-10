using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevicesManagementData.Migrations
{
    /// <inheritdoc />
    public partial class addSimplePkToUserDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices");

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("12161baa-f126-4211-b8cd-23a7b535fbc7"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("5b2edfe4-0c00-48bf-9a4a-fef366810789"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("ca7dc133-d5f6-47cd-86ec-551f0250633b"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("dc26c5e2-91b2-4193-a044-f48e6b1e3a9f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0be6f0cf-d278-4591-bc10-500d2565ad09"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("434be442-555c-4991-8cac-9c4fa3e4b4a4"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserDevices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "MaximuHourlyEnergyConsumption", "Name" },
                values: new object[,]
                {
                    { new Guid("9719778b-f8c7-4a7c-bcd6-893bcf04c9db"), "eco-friendly", 10L, "Air Conditioner" },
                    { new Guid("9da63984-b01f-4f57-a360-c6f9e3277f20"), "store rainwater.", 3L, "Rain barrels" },
                    { new Guid("eb593e4f-1dff-4ef3-acd4-55ed1f69c819"), "just an inch of water a week", 1L, " Smart sprinkler" },
                    { new Guid("faf982b2-c26a-45e5-b794-d3246db752a1"), "An energy-efficient dishwasher", 6L, "Dishwasher" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("439a5955-08b0-4556-8569-0388dc30e0eb"), "razvanb@yahoo.com", "Razvan" },
                    { new Guid("b634507d-6eed-4f20-aef1-7c5647973523"), "floreaneusebiu@gmail.com", "Eusebiu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices");

            migrationBuilder.DropIndex(
                name: "IX_UserDevices_UserId",
                table: "UserDevices");

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("9719778b-f8c7-4a7c-bcd6-893bcf04c9db"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("9da63984-b01f-4f57-a360-c6f9e3277f20"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("eb593e4f-1dff-4ef3-acd4-55ed1f69c819"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("faf982b2-c26a-45e5-b794-d3246db752a1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("439a5955-08b0-4556-8569-0388dc30e0eb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b634507d-6eed-4f20-aef1-7c5647973523"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserDevices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices",
                columns: new[] { "UserId", "DeviceId" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "MaximuHourlyEnergyConsumption", "Name" },
                values: new object[,]
                {
                    { new Guid("12161baa-f126-4211-b8cd-23a7b535fbc7"), "just an inch of water a week", 1L, " Smart sprinkler" },
                    { new Guid("5b2edfe4-0c00-48bf-9a4a-fef366810789"), "eco-friendly", 10L, "Air Conditioner" },
                    { new Guid("ca7dc133-d5f6-47cd-86ec-551f0250633b"), "store rainwater.", 3L, "Rain barrels" },
                    { new Guid("dc26c5e2-91b2-4193-a044-f48e6b1e3a9f"), "An energy-efficient dishwasher", 6L, "Dishwasher" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("0be6f0cf-d278-4591-bc10-500d2565ad09"), "razvanb@yahoo.com", "Razvan" },
                    { new Guid("434be442-555c-4991-8cac-9c4fa3e4b4a4"), "floreaneusebiu@gmail.com", "Eusebiu" }
                });
        }
    }
}
