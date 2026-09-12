using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EventsFromInstagram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Otvorenje nove MINT sezone uz DJ Spectacle.", true, "MINT Season Opening" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 325,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Club H2O otvara 16. sezonu Hrama zabave. Rezervacije preko BRIA platforme.", true, "Otvorenje 16. sezone — Hram zabave" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 327,
                columns: new[] { "Description", "IsFeatured", "Name", "StartTime" },
                values: new object[] { "Maya Berović u MINT-u, početak u 22:00. Rezervacije obavezne.", true, "MINT vikend uz Mayu Berović", new DateTime(2026, 9, 18, 22, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 337,
                column: "AgeLimit",
                value: 19);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 355,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Još jedno izdanje Grčke večeri — mediteranska atmosfera, ples i tanjuri za razbijanje.", true, "Grčka večer u H2O" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 367,
                column: "AgeLimit",
                value: 19);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 400, 19, null, "Srijeda u Osjećaju — stalni tjedni program kluba.", new DateTime(2026, 9, 17, 3, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "xXx Osjećaj Srijeda", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 16, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 401, 19, null, "Subotnja noć u Osjećaju.", new DateTime(2026, 9, 20, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Osjećaj Subota", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 19, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 402, 19, null, "Srijeda u Osjećaju — stalni tjedni program kluba.", new DateTime(2026, 9, 24, 3, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "xXx Osjećaj Srijeda", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 23, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 403, 19, null, "Subotnja noć u Osjećaju.", new DateTime(2026, 9, 27, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Osjećaj Subota", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 26, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 404, 18, null, "Magazin otvara novu sezonu u Club & Lounge Roko — večer punu hitova.", new DateTime(2026, 10, 3, 4, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "MAGAZIN u Roku", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", new DateTime(2026, 10, 2, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 303,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "House i techno u vrtu kluba Mint.", false, "Mint Saturday Garden" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 325,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Petak u H2O — komercijalni hitovi i VIP zona.", false, "H2O Friday Pure Fun" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 327,
                columns: new[] { "Description", "IsFeatured", "Name", "StartTime" },
                values: new object[] { "House u vrtu kluba Mint.", false, "Mint Friday Garden", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 337,
                column: "AgeLimit",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 355,
                columns: new[] { "Description", "IsFeatured", "Name" },
                values: new object[] { "Petak u H2O.", false, "H2O Friday Pure Fun" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 367,
                column: "AgeLimit",
                value: 18);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 300, 18, null, "Otvorenje sezone u H2O — rezidenti i gostujući DJ do jutra.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, true, "H2O Season Opening", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 301, 18, null, "Povratak sezone u Katran — techno maraton u industrijskom ambijentu.", new DateTime(2026, 9, 13, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, true, "Katran: Season Opening Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 302, 18, null, "Subota u Ritzu uz rezidentne DJ-eve.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 313, 18, null, "Domaća zabava i hitovi u Club & Lounge Roko.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Fešta", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 },
                    { 321, 18, null, "Rezidentna techno večer.", new DateTime(2026, 9, 18, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Katran Thursday Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 322, 18, null, "R'n'B i hip-hop selekcija uz koktele.", new DateTime(2026, 9, 18, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "H2O Thursday Vibes", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 323, 18, null, "Slijedi osjećaj — domaći i strani hitovi.", new DateTime(2026, 9, 18, 4, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Osjećaj Četvrtak", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 17, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 326, 18, null, "Glamurozna noć uz rezidentne DJ-eve.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Friday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 335, 18, null, "Domaći hitovi i zabava.", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Vikend Fešta", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 },
                    { 340, 18, null, "Subota u Ritzu.", new DateTime(2026, 9, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 356, 18, null, "Glamurozni petak.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Friday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 373, 18, null, "Subota u Ritzu.", new DateTime(2026, 9, 27, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 }
                });
        }
    }
}
