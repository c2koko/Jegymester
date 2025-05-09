using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jegymester.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class chair_solved_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_TicketId",
                table: "Chairs",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Tickets_TicketId",
                table: "Chairs",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Tickets_TicketId",
                table: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_TicketId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Chairs");
        }
    }
}
