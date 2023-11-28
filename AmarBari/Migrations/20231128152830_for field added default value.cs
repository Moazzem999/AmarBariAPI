using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmarBari.Migrations
{
    /// <inheritdoc />
    public partial class forfieldaddeddefaultvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BillPerUnit",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentMonthUnit",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ElectricityBill",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GasBill",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HouseRent",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OthersBill",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousMonthUnit",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceCharge",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitUsed",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterBill",
                table: "MonthlyBills",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillPerUnit",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "CurrentMonthUnit",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "ElectricityBill",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "GasBill",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "HouseRent",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "OthersBill",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "PreviousMonthUnit",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "ServiceCharge",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "UnitUsed",
                table: "MonthlyBills");

            migrationBuilder.DropColumn(
                name: "WaterBill",
                table: "MonthlyBills");
        }
    }
}
