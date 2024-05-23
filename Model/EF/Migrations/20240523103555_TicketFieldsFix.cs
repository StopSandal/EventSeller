using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventSeller.Migrations
{
    /// <inheritdoc />
    public partial class TicketFieldsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketEndEventDateTime",
                table: "Tickets",
                newName: "TicketEndDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketEndDateTime",
                table: "Tickets",
                newName: "TicketEndEventDateTime");
        }
    }
}
