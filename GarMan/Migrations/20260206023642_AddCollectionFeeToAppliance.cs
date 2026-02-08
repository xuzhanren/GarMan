using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarMan.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionFeeToAppliance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CollectionFee",
                table: "Appliances",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 50m);

            // Update existing records to have the default value of 50
            migrationBuilder.Sql("UPDATE Appliances SET CollectionFee = 50 WHERE CollectionFee = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionFee",
                table: "Appliances");
        }
    }
}
