using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_Project.Migrations
{
    /// <inheritdoc />
    public partial class AlterShipmentDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrackingNumber",
                table: "ShipmentDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrackingNumber",
                table: "ShipmentDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
