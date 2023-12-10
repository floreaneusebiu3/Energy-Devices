using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevicesManagementData.Migrations
{
    /// <inheritdoc />
    public partial class addFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserDeviceDeviceId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserDeviceUserId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AddressId",
                table: "Devices",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserDeviceUserId_UserDeviceDeviceId",
                table: "Devices",
                columns: new[] { "UserDeviceUserId", "UserDeviceDeviceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Addresses_AddressId",
                table: "Devices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_UserDevices_UserDeviceUserId_UserDeviceDeviceId",
                table: "Devices",
                columns: new[] { "UserDeviceUserId", "UserDeviceDeviceId" },
                principalTable: "UserDevices",
                principalColumns: new[] { "UserId", "DeviceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Addresses_AddressId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_UserDevices_UserDeviceUserId_UserDeviceDeviceId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_AddressId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserDeviceUserId_UserDeviceDeviceId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserDeviceDeviceId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserDeviceUserId",
                table: "Devices");
        }
    }
}
