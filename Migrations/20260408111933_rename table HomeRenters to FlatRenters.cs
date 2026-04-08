using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmarBariAPI.Migrations
{
    /// <inheritdoc />
    public partial class renametableHomeRenterstoFlatRenters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeRenters");

            migrationBuilder.CreateTable(
                name: "FlatRenters",
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
                    table.PrimaryKey("PK_FlatRenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatRenters_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatRenters_FlatId",
                table: "FlatRenters",
                column: "FlatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatRenters");

            migrationBuilder.CreateTable(
                name: "HomeRenters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<long>(type: "bigint", nullable: false),
                    AcademicQualification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdvancedPaymet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NidNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PresentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
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
                name: "IX_HomeRenters_FlatId",
                table: "HomeRenters",
                column: "FlatId");
        }
    }
}
