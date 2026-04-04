using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group3Flight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightCode = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<string>(type: "TEXT", nullable: false),
                    To = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    CabinType = table.Column<string>(type: "TEXT", nullable: false),
                    Emission = table.Column<double>(type: "REAL", nullable: false),
                    AircraftType = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flight_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airline",
                columns: new[] { "AirlineId", "ImageName", "Name" },
                values: new object[,]
                {
                    { 1, "emirates.png", "Emirates" },
                    { 2, "qatar_airways.png", "Qatar Airways" },
                    { 3, "lufthansa.png", "Lufthansa" },
                    { 4, "air_france.png", "Air France" },
                    { 5, "singapore_airlines.png", "Singapore Airlines" },
                    { 6, "etihad_airways.png", "Etihad Airways" }
                });

            migrationBuilder.InsertData(
                table: "Flight",
                columns: new[] { "FlightId", "AircraftType", "AirlineId", "ArrivalTime", "CabinType", "Date", "DepartureTime", "Emission", "FlightCode", "From", "Price", "To" },
                values: new object[,]
                {
                    { 1, "Boeing 777", 2, new TimeSpan(0, 8, 30, 0, 0), "Business", new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0), 650.5, "EK601", "Dubai", 1500, "New York" },
                    { 2, "Boeing 787 Dreamliner", 1, new TimeSpan(0, 14, 45, 0, 0), "Economy", new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 15, 0, 0), 420.30000000000001, "QR702", "Doha", 800, "London" },
                    { 3, "Boeing 747", 4, new TimeSpan(0, 16, 30, 0, 0), "Economy Plus", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), 700.0, "LH803", "Frankfurt", 1800, "Chicago" },
                    { 4, "Airbus A380", 3, new TimeSpan(0, 15, 0, 0, 0), "Economy", new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 30, 0, 0), 500.0, "AF904", "Paris", 950, "Dubai" },
                    { 5, "Airbus A350", 5, new TimeSpan(0, 6, 30, 0, 0), "Business", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 0, 0, 0), 900.0, "SQ1005", "Singapore", 2000, "Dubai" },
                    { 6, "Boeing 787", 6, new TimeSpan(0, 10, 45, 0, 0), "Business", new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 15, 0, 0), 950.0, "EY1106", "Abu Dhabi", 2200, "Sydney" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineId",
                table: "Flight",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FlightId",
                table: "Reservation",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Airline");
        }
    }
}
