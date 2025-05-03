using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jegymester.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class RoomName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreeningLocation",
                table: "Screenings");

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "ScreeningLocation",
                table: "Screenings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
