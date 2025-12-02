using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateconstrain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbUserSebders",
                table: "TbShipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShipingPackgingId",
                table: "TbShipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbUserSenders",
                table: "TbShipments",
                column: "SenderId",
                principalTable: "TbUserSenders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbUserSenders",
                table: "TbShipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShipingPackgingId",
                table: "TbShipments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbUserSebders",
                table: "TbShipments",
                column: "SenderId",
                principalTable: "TbUserSenders",
                principalColumn: "Id");
        }
    }
}
