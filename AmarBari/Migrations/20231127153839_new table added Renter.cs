using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmarBari.Migrations
{
    /// <inheritdoc />
    public partial class newtableaddedRenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RenterId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlatId = table.Column<long>(type: "bigint", nullable: false),
                    RenterName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    AcademicQualification = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NidNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    DocumentImageUrl = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdvancedPaymet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renters_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Renters_FlatId",
                table: "Renters",
                column: "FlatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Renters");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Users");
        }
    }
}
