using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarMan.Migrations
{
    /// <inheritdoc />
    public partial class AddEstimatedAndRecycledValueToAppliance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedValue",
                table: "Appliances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RecycledValue",
                table: "Appliances",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedValue",
                table: "Appliances");

            migrationBuilder.DropColumn(
                name: "RecycledValue",
                table: "Appliances");
        }
    }
}
