using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelNTourism.Migrations
{
    /// <inheritdoc />
    public partial class addRestauranttblUpdateToCld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room1ImgUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room2mgUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room3mgUrl",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Room1ImgUrl",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Room2mgUrl",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Room3mgUrl",
                table: "Restaurants");
        }
    }
}
