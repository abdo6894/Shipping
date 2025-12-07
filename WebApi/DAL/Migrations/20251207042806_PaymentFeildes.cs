using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PaymentFeildes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbPaymentMethods",
                table: "TbShipments");

            migrationBuilder.DropTable(
                name: "TbPaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_TbShipments_PaymentMethodId",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "TbShipments");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "TbShipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentGateway",
                table: "TbShipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentReference",
                table: "TbShipments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "PaymentGateway",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "PaymentReference",
                table: "TbShipments");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "TbShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Commission = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    MethdAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MethodEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_PaymentMethodId",
                table: "TbShipments",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbPaymentMethods",
                table: "TbShipments",
                column: "PaymentMethodId",
                principalTable: "TbPaymentMethods",
                principalColumn: "Id");
        }
    }
}
