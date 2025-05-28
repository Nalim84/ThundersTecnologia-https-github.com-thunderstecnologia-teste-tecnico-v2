using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.OutOfBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateReporTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopGrossingTollPlazasReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TollName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TollTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopGrossingTollPlazasReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopGrossingTollPlazasReports_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TotalPerHourPerCityReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    TotalPerHour = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalPerHourPerCityReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalPerHourPerCityReports_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypesPerTollPlazaReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypesPerTollPlazaReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypesPerTollPlazaReports_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopGrossingTollPlazasReports_ReportId",
                table: "TopGrossingTollPlazasReports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TotalPerHourPerCityReports_ReportId",
                table: "TotalPerHourPerCityReports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypesPerTollPlazaReports_ReportId",
                table: "VehicleTypesPerTollPlazaReports",
                column: "ReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopGrossingTollPlazasReports");

            migrationBuilder.DropTable(
                name: "TotalPerHourPerCityReports");

            migrationBuilder.DropTable(
                name: "VehicleTypesPerTollPlazaReports");
        }
    }
}
