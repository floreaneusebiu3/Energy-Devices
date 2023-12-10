using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevicesManagementData.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("437ff9e8-4486-400f-bdd7-0ed41d4fafc3"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("d247048b-4f67-40ad-a4eb-5ba66c86b121"));

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
        }
    }
}
