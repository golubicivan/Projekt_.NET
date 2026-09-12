using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InstagramLogosAndSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Tradicionalni party koji se u Sovi održava jednom godišnje još od 2016.", true, "WELCOME TO THE JUNGLE" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 310,
                column: "Description",
                value: "Jedan podrum, tri svijeta: BUNKER · VAULT · SKETCH.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 333,
                column: "Description",
                value: "Jedan podrum, tri svijeta: BUNKER · VAULT · SKETCH.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 363,
                column: "Description",
                value: "Jedan podrum, tri svijeta: BUNKER · VAULT · SKETCH.");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 500, 18, null, "Povratak u osamdesete — hitovi iz zlatnih vremena na dva dance floora.", new DateTime(2026, 9, 20, 4, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, true, "Old but Gold", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", new DateTime(2026, 9, 19, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 501, 18, null, "Subotnji party u Sovi, vrata u 22:00.", new DateTime(2026, 9, 27, 4, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Subota", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 26, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 502, 18, null, "Cro trending hitovi, balkan trap i domaće na krovu Ilice 16.", new DateTime(2026, 9, 19, 3, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "HERC: MUFASA × MINDMVE", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 503, 0, null, "Utorak s mikrofonom: karaoke i kviz uz pivo na špici.", new DateTime(2026, 9, 15, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Utorak — karaoke & kviz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 9, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 504, 0, null, "Utorak s mikrofonom: karaoke i kviz uz pivo na špici.", new DateTime(2026, 9, 22, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Utorak — karaoke & kviz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 9, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 505, 0, null, "Utorak na terasi uz bachatu i salsu — ulaz besplatan.", new DateTime(2026, 9, 15, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Aquarius SUNSET: Bachata & Salsa", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 9, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 506, 0, null, "Utorak na terasi uz bachatu i salsu — ulaz besplatan.", new DateTime(2026, 9, 22, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Aquarius SUNSET: Bachata & Salsa", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 9, 22, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 507, 18, null, "Elegantni petak u THE Clubu (dress code: elegant).", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Friday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 508, 18, null, "Elegantni petak u THE Clubu (dress code: elegant).", new DateTime(2026, 9, 26, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Friday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 }
                });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                column: "InstagramUrl",
                value: "https://www.instagram.com/aquariuszagreb/");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                column: "InstagramUrl",
                value: "https://www.instagram.com/katranzagreb/");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "https://www.instagram.com/operazagreb/", "/img/logos/opera-emblem.jpg" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                column: "LogoUrl",
                value: "/img/logos/masters-emblem.jpg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 27,
                column: "LogoUrl",
                value: "/img/logos/osjecaj-emblem.jpg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "https://www.instagram.com/bulldog_zagreb/", "/img/logos/bulldog-emblem.jpg" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "https://www.instagram.com/outbunker/", "/img/logos/outbunker-emblem.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 308,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Vikend party u Sovi — do zore.", false, "Sova Weekend Party" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 310,
                column: "Description",
                value: "Underground techno u bunkeru ispod Ilice.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 333,
                column: "Description",
                value: "Deep i techno u bunkeru.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 363,
                column: "Description",
                value: "Techno u bunkeru.");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 317, 0, null, "Tjedni kviz znanja na špici — ekipe do 6 igrača.", new DateTime(2026, 9, 14, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 9, 14, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 334, 18, null, "Vikend u Sovi — do zore.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Night Fever", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 348, 0, null, "Tjedni kviz znanja — nova runda pitanja.", new DateTime(2026, 9, 21, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 9, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 364, 18, null, "Vikend u Sovi.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Weekend Party", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 25, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 }
                });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                column: "InstagramUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                column: "InstagramUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "", "/img/logos/opera.svg" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                column: "LogoUrl",
                value: "/img/logos/masters.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 27,
                column: "LogoUrl",
                value: "/img/logos/osjecaj-emblem.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "", "/img/logos/bulldog.svg" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "", "/img/logos/outbunker.svg" });
        }
    }
}
