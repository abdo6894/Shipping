using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatecarrier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipmentStatus_TbCarriers",
                table: "TbShipmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_TbShipmentStatus_CarrierId",
                table: "TbShipmentStatus");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "TbShipmentStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "TbShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_CarrierId",
                table: "TbShipments",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbCarriers",
                table: "TbShipments",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbCarriers",
                table: "TbShipments");

            migrationBuilder.DropIndex(
                name: "IX_TbShipments_CarrierId",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "TbShipments");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrierId",
                table: "TbShipmentStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TbShipmentStatus_CarrierId",
                table: "TbShipmentStatus",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipmentStatus_TbCarriers",
                table: "TbShipmentStatus",
                column: "CarrierId",
                principalTable: "TbCarriers",
                principalColumn: "Id");
        }
    }
}
