using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.OutOfBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateReportTopNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "TopGrossingTollPlazasReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "TopGrossingTollPlazasReports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "TopGrossingTollPlazasReports");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "TopGrossingTollPlazasReports");
        }
    }
}
