using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SedamNovihKlubova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "ContactPhone", "DeletedAt", "Description", "ImageUrl", "Latitude", "LogoUrl", "Longitude", "Name", "OwnerAppUserId", "Type", "WorkingHours" },
                values: new object[,]
                {
                    { 25, "Runjaninova 3", 600, "+385915100661", null, "Ekskluzivni noćni klub u centru Zagreba — priča koja drži vodu. 'Pure fun'.", "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=800", 45.8035, "/img/logos/h2o-emblem.jpg", 15.971500000000001, "Club H2O", null, 0, "23:00 - 06:00" },
                    { 26, "Izidora Kršnjavoga 1", 500, "+385981112233", null, "Živahan klub u Donjem gradu s balkan i pop programom.", "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800", 45.805199999999999, "/img/logos/ex.svg", 15.966799999999999, "EX Club", null, 0, "23:00 - 05:00" },
                    { 27, "Kačićeva 23", 350, "+385986700322", null, "Klub i caffe bar u starom dijelu grada — slijedi osjećaj.", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=800", 45.808599999999998, "/img/logos/osjecaj.svg", 15.964600000000001, "Osjećaj", null, 0, "22:00 - 05:00" },
                    { 28, "Florijana Andrašeca 14", 700, "+385985525500", null, "Glamurozni noćni klub — najbolji doživljaj zagrebačkog noćnog života.", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=800", 45.809199999999997, "/img/logos/ritz.svg", 15.956200000000001, "Ritz Club", null, 0, "23:00 - 06:00" },
                    { 29, "Bogovićeva 1a", 400, "+385991658675", null, "Elegantni klub u samom centru — 'The' place to be.", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=800", 45.811399999999999, "/img/logos/theclub.svg", 15.975899999999999, "THE Club", null, 0, "23:00 - 05:00" },
                    { 30, "Florijana Andrašeca 14", 800, "+385913900707", null, "Klub, vrt i više od toga — mint & more.", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800", 45.810000000000002, "/img/logos/mint-emblem.jpg", 15.9556, "Mint Club & More", null, 0, "23:00 - 06:00" },
                    { 31, "Horvaćanska cesta 17a", 900, "+385976592000", null, "Klub i lounge stvoren za zabavu.", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800", 45.795999999999999, "/img/logos/roko-emblem.jpg", 15.945, "Club & Lounge Roko", null, 0, "23:00 - 06:00" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 33, 18, null, "Veliko otvorenje sezone u Clubu H2O uz top DJ-eve.", new DateTime(2026, 7, 11, 6, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "H2O Pure Fun Opening", "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", new DateTime(2026, 7, 10, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 34, 18, null, "Najbolji balkan i pop hitovi cijelu noć.", new DateTime(2026, 7, 18, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "EX Balkan Night", "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=600", new DateTime(2026, 7, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 26 },
                    { 35, 18, null, "Live nastup uz koktele u klubu Osjećaj.", new DateTime(2026, 7, 10, 3, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Osjećaj Live Session", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 7, 9, 22, 0, 0, 0, DateTimeKind.Unspecified), 1, 27 },
                    { 36, 18, null, "Glamurozna subotnja noć uz rezidentne DJ-eve.", new DateTime(2026, 7, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 7, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 37, 18, null, "Otvaranje sezone u najekskluzivnijem klubu centra.", new DateTime(2026, 7, 12, 5, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, false, "THE Opening Party", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 7, 11, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 38, 18, null, "House i techno u vrtu kluba Mint.", new DateTime(2026, 7, 25, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Garden Sessions", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 39, 18, null, "Domaća zabava i hitovi u Club & Lounge Roko.", new DateTime(2026, 7, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Fešta", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", new DateTime(2026, 7, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 31);
        }
    }
}
