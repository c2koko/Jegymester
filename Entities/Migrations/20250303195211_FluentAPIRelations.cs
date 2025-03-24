using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jegymester.Migrations
{
    /// <inheritdoc />
    public partial class FluentAPIRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomsChairs_Chair_ChairId",
                table: "RoomsChairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Screening_Movies_MovieId",
                table: "Screening");

            migrationBuilder.DropForeignKey(
                name: "FK_Screening_Rooms_RoomId",
                table: "Screening");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Screening_ScreeningId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomsChairs",
                table: "RoomsChairs");

            migrationBuilder.DropIndex(
                name: "IX_RoomsChairs_RoomId",
                table: "RoomsChairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screening",
                table: "Screening");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chair",
                table: "Chair");

            migrationBuilder.RenameTable(
                name: "Screening",
                newName: "Screenings");

            migrationBuilder.RenameTable(
                name: "Chair",
                newName: "Chairs");

            migrationBuilder.RenameIndex(
                name: "IX_Screening_RoomId",
                table: "Screenings",
                newName: "IX_Screenings_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Screening_MovieId",
                table: "Screenings",
                newName: "IX_Screenings_MovieId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RoomsChairs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "MovieDescription",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomsChairs",
                table: "RoomsChairs",
                columns: new[] { "RoomId", "ChairId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screenings",
                table: "Screenings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chairs",
                table: "Chairs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Id",
                table: "Tickets",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Id",
                table: "Rooms",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_Id",
                table: "Screenings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_Id",
                table: "Chairs",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsChairs_Chairs_ChairId",
                table: "RoomsChairs",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Rooms_RoomId",
                table: "Screenings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Screenings_ScreeningId",
                table: "Tickets",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomsChairs_Chairs_ChairId",
                table: "RoomsChairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Rooms_RoomId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Screenings_ScreeningId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Id",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomsChairs",
                table: "RoomsChairs");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_Id",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screenings",
                table: "Screenings");

            migrationBuilder.DropIndex(
                name: "IX_Screenings_Id",
                table: "Screenings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chairs",
                table: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_Id",
                table: "Chairs");

            migrationBuilder.RenameTable(
                name: "Screenings",
                newName: "Screening");

            migrationBuilder.RenameTable(
                name: "Chairs",
                newName: "Chair");

            migrationBuilder.RenameIndex(
                name: "IX_Screenings_RoomId",
                table: "Screening",
                newName: "IX_Screening_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Screenings_MovieId",
                table: "Screening",
                newName: "IX_Screening_MovieId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RoomsChairs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "MovieDescription",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomsChairs",
                table: "RoomsChairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screening",
                table: "Screening",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chair",
                table: "Chair",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomsChairs_RoomId",
                table: "RoomsChairs",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsChairs_Chair_ChairId",
                table: "RoomsChairs",
                column: "ChairId",
                principalTable: "Chair",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screening_Movies_MovieId",
                table: "Screening",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Screening_Rooms_RoomId",
                table: "Screening",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Screening_ScreeningId",
                table: "Tickets",
                column: "ScreeningId",
                principalTable: "Screening",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
