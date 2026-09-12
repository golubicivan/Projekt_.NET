using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class KatranSeasonOpening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[] { 405, 19, null, "Otvorenje sezone u Katranu — 4 dance floora, vrata u 23:00.", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Katran Season Opening", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 353, 18, null, "Rezidenti i gost u podrumu Katrana.", new DateTime(2026, 9, 25, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Katran Underground: Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 354, 18, null, "R'n'B i hip-hop selekcija.", new DateTime(2026, 9, 25, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "H2O Thursday Vibes", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 }
                });
        }
    }
}
