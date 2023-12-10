using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevicesManagementData.Migrations
{
    /// <inheritdoc />
    public partial class addAddressAsString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDevices_Addresses_AddressId",
                table: "UserDevices");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_UserDevices_AddressId",
                table: "UserDevices");

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("65911a4f-b4c1-41bc-9055-428c1f6eef50"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("736aafec-3b03-44fb-82cd-9659ac324590"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("75704484-90e2-459d-83f8-dc31c597e4b4"));

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("7ee04232-7e0b-4727-90bd-f6f20980ba7c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("16bf1586-6195-441e-9ec1-c2a05373388a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("869b2319-f986-41a2-bd1a-a6966599d062"));

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "UserDevices");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserDevices");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "UserDevices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Street" },
                values: new object[,]
                {
                    { new Guid("437ff9e8-4486-400f-bdd7-0ed41d4fafc3"), "Lincoln", "United States", "Main Street" },
                    { new Guid("d247048b-4f67-40ad-a4eb-5ba66c86b121"), "Lincoln", "United States", "Main Street" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "MaximuHourlyEnergyConsumption", "Name" },
                values: new object[,]
                {
                    { new Guid("65911a4f-b4c1-41bc-9055-428c1f6eef50"), "store rainwater.", 3L, "Rain barrels" },
                    { new Guid("736aafec-3b03-44fb-82cd-9659ac324590"), "An energy-efficient dishwasher", 6L, "Dishwasher" },
                    { new Guid("75704484-90e2-459d-83f8-dc31c597e4b4"), "eco-friendly", 10L, "Air Conditioner" },
                    { new Guid("7ee04232-7e0b-4727-90bd-f6f20980ba7c"), "just an inch of water a week", 1L, " Smart sprinkler" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("16bf1586-6195-441e-9ec1-c2a05373388a"), "razvanb@yahoo.com", "Razvan" },
                    { new Guid("869b2319-f986-41a2-bd1a-a6966599d062"), "floreaneusebiu@gmail.com", "Eusebiu" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_AddressId",
                table: "UserDevices",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevices_Addresses_AddressId",
                table: "UserDevices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
