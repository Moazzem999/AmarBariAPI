using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmarBariAPI.Migrations
{
    /// <inheritdoc />
    public partial class newtableaddedHomesFlatsHomeRenters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractEntity_ShopRenterEntity_ShopRenterId",
                table: "ContractEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopRenterEntity_Shops_ShopId",
                table: "ShopRenterEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopRenterEntity",
                table: "ShopRenterEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractEntity",
                table: "ContractEntity");

            migrationBuilder.RenameTable(
                name: "ShopRenterEntity",
                newName: "ShopRenters");

            migrationBuilder.RenameTable(
                name: "ContractEntity",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_ShopRenterEntity_ShopId",
                table: "ShopRenters",
                newName: "IX_ShopRenters_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractEntity_ShopRenterId",
                table: "Contracts",
                newName: "IX_Contracts_ShopRenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopRenters",
                table: "ShopRenters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CurrentRent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GasBill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterBill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OthersBill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flats_Homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeRenters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NidNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AcademicQualification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PresentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    AdvancedPaymet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeRenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeRenters_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flats_HomeId",
                table: "Flats",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeRenters_FlatId",
                table: "HomeRenters",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_UserId",
                table: "Homes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ShopRenters_ShopRenterId",
                table: "Contracts",
                column: "ShopRenterId",
                principalTable: "ShopRenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopRenters_Shops_ShopId",
                table: "ShopRenters",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ShopRenters_ShopRenterId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopRenters_Shops_ShopId",
                table: "ShopRenters");

            migrationBuilder.DropTable(
                name: "HomeRenters");

            migrationBuilder.DropTable(
                name: "Flats");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopRenters",
                table: "ShopRenters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "ShopRenters",
                newName: "ShopRenterEntity");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "ContractEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ShopRenters_ShopId",
                table: "ShopRenterEntity",
                newName: "IX_ShopRenterEntity_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ShopRenterId",
                table: "ContractEntity",
                newName: "IX_ContractEntity_ShopRenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopRenterEntity",
                table: "ShopRenterEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractEntity",
                table: "ContractEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractEntity_ShopRenterEntity_ShopRenterId",
                table: "ContractEntity",
                column: "ShopRenterId",
                principalTable: "ShopRenterEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopRenterEntity_Shops_ShopId",
                table: "ShopRenterEntity",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
