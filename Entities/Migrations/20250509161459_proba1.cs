using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jegymester.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class proba1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Rooms_RoomId",
                table: "Chairs");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Chairs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ScreeningId",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_ScreeningId",
                table: "Chairs",
                column: "ScreeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Rooms_RoomId",
                table: "Chairs",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Screenings_ScreeningId",
                table: "Chairs",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Rooms_RoomId",
                table: "Chairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Screenings_ScreeningId",
                table: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_ScreeningId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "ScreeningId",
                table: "Chairs");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Rooms_RoomId",
                table: "Chairs",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
