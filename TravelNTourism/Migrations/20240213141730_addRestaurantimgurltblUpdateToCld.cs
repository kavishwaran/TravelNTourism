using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelNTourism.Migrations
{
    /// <inheritdoc />
    public partial class addRestaurantimgurltblUpdateToCld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room4ImgUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room5mgUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room6mgUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room4ImgUrl",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Room5mgUrl",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Room6mgUrl",
                table: "Restaurants");
        }
    }
}
