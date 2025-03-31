using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jegymester.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class ChairImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomsChairs");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_Id",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_Id",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "Collumm",
                table: "Chairs");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Chairs",
                newName: "RoomId");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Chairs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_RoomId",
                table: "Chairs",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Rooms_RoomId",
                table: "Chairs",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Rooms_RoomId",
                table: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_RoomId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Chairs");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Chairs",
                newName: "Row");

            migrationBuilder.AddColumn<int>(
                name: "Collumm",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomsChairs",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ChairId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Reserved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsChairs", x => new { x.RoomId, x.ChairId });
                    table.ForeignKey(
                        name: "FK_RoomsChairs_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomsChairs_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Id",
                table: "Rooms",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_Id",
                table: "Chairs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomsChairs_ChairId",
                table: "RoomsChairs",
                column: "ChairId");
        }
    }
}
