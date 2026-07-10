using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserProvidedCoordinatesAndNewClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.79862, 15.970980000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.791080000000001, 15.97639 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Address", "Latitude", "Longitude" },
                values: new object[] { "V. Ravnice 10", 45.82273, 16.032889999999998 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Address", "Latitude", "Longitude" },
                values: new object[] { "Florijana Andrašeca 14", 45.801630000000003, 15.96095 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.808430000000001, 15.964219999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.801450000000003, 15.96091 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.812220000000003, 15.974830000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.801560000000002, 15.96129 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Address", "Latitude", "Longitude" },
                values: new object[] { "Jarunska ulica", 45.78387, 15.94783 });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "ContactPhone", "DeletedAt", "Description", "FloorPlanUrl", "ImageUrl", "InstagramUrl", "Latitude", "LogoUrl", "Longitude", "Name", "OwnerAppUserId", "Type", "WorkingHours" },
                values: new object[,]
                {
                    { 101, "Vlaška 9", 300, "+385911230032", null, "Cocktail i party klub u Vlaškoj ulici.", "", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=800", "", 45.813639999999999, "/img/logos/xo.svg", 15.979789999999999, "XO Club", null, 0, "23:00 - 05:00" },
                    { 102, "Adančeva ulica, Brckovljani (Dugo Selo)", 400, "+385911230033", null, "Noćni klub istočno od Zagreba — party do zore.", "", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800", "", 45.826160000000002, "/img/logos/sova.svg", 16.303609999999999, "Noćni klub Sova", null, 0, "22:00 - 06:00" },
                    { 103, "Andrije Hebranga 14", 250, "+385911230034", null, "Noćni klub u srcu Donjeg grada.", "", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=800", "", 45.809429999999999, "/img/logos/rocco.svg", 15.97564, "Night Club Rocco", null, 0, "23:00 - 06:00" },
                    { 104, "Preradovićeva 12", 200, "+385911230035", null, "Rock bar i noćni klub — glasne gitare do jutra.", "", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800", "", 45.81071, "/img/logos/alcatraz.svg", 15.97428, "Alcatraz Rock Bar", null, 0, "21:00 - 04:00" },
                    { 105, "Bogovićeva 6", 300, "+385911230036", null, "Legendarni pub i bar na špici.", "", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=800", "", 45.812309999999997, "/img/logos/bulldog.svg", 15.97518, "Bulldog Zagreb", null, 1, "09:00 - 02:00" },
                    { 106, "Ilica 16", 350, "+385911230037", null, "Underground klub u bunkeru ispod Ilice.", "", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=800", "", 45.813450000000003, "/img/logos/outbunker.svg", 15.97357, "OUT Bunker Nightclub", null, 0, "23:00 - 06:00" },
                    { 107, "Ilica 16", 200, "+385911230038", null, "Rooftop bar s pogledom na krovove Zagreba.", "", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=800", "", 45.813200000000002, "/img/logos/outrooftop.svg", 15.9735, "OUT Rooftop", null, 1, "20:00 - 02:00" },
                    { 108, "Ulica kneza Borne 2", 300, "+385911230039", null, "Skriveni klub za one koji znaju.", "", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800", "", 45.80733, "/img/logos/secret.svg", 15.98414, "The Secret Club", null, 0, "23:00 - 06:00" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 111, 18, null, "Petak navečer u XO klubu uz house i pop hitove.", new DateTime(2026, 7, 18, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "XO Friday Party", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 7, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 101 },
                    { 112, 18, null, "Vikend party u Sovi — do zore.", new DateTime(2026, 7, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Night Fever", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", new DateTime(2026, 7, 18, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 113, 18, null, "Vikend zabava u Roccu.", new DateTime(2026, 7, 18, 6, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Rocco Weekend Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 7, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 103 },
                    { 114, 0, null, "Živa rock svirka i glasne gitare.", new DateTime(2026, 7, 17, 2, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Alcatraz Rock Night", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 7, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 104 },
                    { 115, 0, null, "Tjedni kviz znanja na špici — ekipe do 6 igrača.", new DateTime(2026, 7, 15, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 7, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 116, 18, null, "Underground techno u bunkeru ispod Ilice.", new DateTime(2026, 7, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Bunker Techno Session", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 7, 18, 23, 30, 0, 0, DateTimeKind.Unspecified), 0, 106 },
                    { 117, 0, null, "Zalazak sunca uz DJ-a na krovu Ilice 16.", new DateTime(2026, 7, 17, 1, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, false, "Rooftop Sunset DJ", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 7, 16, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 118, 18, null, "Subota koja se ne priča dalje.", new DateTime(2026, 7, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Secret Saturday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 108 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.798940000000002, 15.97125 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.783000000000001, 15.9885 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Address", "Latitude", "Longitude" },
                values: new object[] { "Trg Krešimira Ćosića 9", 45.805700000000002, 15.9521 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Address", "Latitude", "Longitude" },
                values: new object[] { "Trnjanska cesta", 45.792999999999999, 15.98 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.808329999999998, 15.964270000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.801609999999997, 15.960750000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.811990000000002, 15.976129999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.801439999999999, 15.960599999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Address", "Latitude", "Longitude" },
                values: new object[] { "Horvaćanska cesta 17a", 45.786999999999999, 15.951700000000001 });
        }
    }
}
