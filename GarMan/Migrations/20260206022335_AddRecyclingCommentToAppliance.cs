using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarMan.Migrations
{
    /// <inheritdoc />
    public partial class AddRecyclingCommentToAppliance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecyclingComment",
                table: "Appliances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecyclingComment",
                table: "Appliances");
        }
    }
}
