using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventSeller.Migrations
{
    /// <inheritdoc />
    public partial class initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartEventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndEventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlaceAddresses",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceAddresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlaceHalls",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventAddressID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceHalls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaceHalls_PlaceAddresses_EventAddressID",
                        column: x => x.EventAddressID,
                        principalTable: "PlaceAddresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallSectors",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceHallID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallSectors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HallSectors_PlaceHalls_PlaceHallID",
                        column: x => x.PlaceHallID,
                        principalTable: "PlaceHalls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceRow = table.Column<int>(type: "int", nullable: true),
                    PlaceSeat = table.Column<int>(type: "int", nullable: true),
                    HallSectorID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Seats_HallSectors_HallSectorID",
                        column: x => x.HallSectorID,
                        principalTable: "HallSectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TicketStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TicketEndEventDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeatID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HallSectors_PlaceHallID",
                table: "HallSectors",
                column: "PlaceHallID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceHalls_EventAddressID",
                table: "PlaceHalls",
                column: "EventAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallSectorID",
                table: "Seats",
                column: "HallSectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventID",
                table: "Tickets",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets",
                column: "SeatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "HallSectors");

            migrationBuilder.DropTable(
                name: "PlaceHalls");

            migrationBuilder.DropTable(
                name: "PlaceAddresses");
        }
    }
}
