using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealZagrebVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 31, 18, null, "Cjelonoćni techno maraton u Club Cultureu.", new DateTime(2026, 7, 20, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, "Summer Techno Marathon", "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", new DateTime(2026, 7, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 32, 0, null, "Tjedni pub kviz u Kavani Lav, ljetno izdanje.", new DateTime(2026, 6, 24, 22, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, "Ljetni Pub Quiz", "https://images.unsplash.com/photo-1606761568499-6d2451b23c66?w=600", new DateTime(2026, 6, 24, 19, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "ContactPhone", "DeletedAt", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OwnerAppUserId", "Type", "WorkingHours" },
                values: new object[,]
                {
                    { 4, "Aleja Matije Ljubeka 2a, Jarun", 1500, "+38513640231", null, "Legendarni klub na jezeru Jarun, dom elektronske glazbe u Zagrebu.", "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=800", 45.783299999999997, 15.918100000000001, "Aquarius", null, 0, "23:00 - 06:00" },
                    { 5, "Ulica grada Vukovara 68", 1200, "+38516313021", null, "Klub i koncertni prostor za domaće i strane izvođače.", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800", 45.801299999999998, 15.9869, "Boogaloo", null, 0, "21:00 - 04:00" },
                    { 6, "Šubićeva 2", 1000, "+38514606650", null, "Koncertni i klupski prostor s bogatim programom uživo.", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=800", 45.814599999999999, 16.000800000000002, "Tvornica Kulture", null, 0, "20:00 - 04:00" },
                    { 7, "Trnjanski nasip bb", 600, "+38516154290", null, "Alternativni klub uz Savu, dom rock i underground scene.", "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=800", 45.795000000000002, 15.976000000000001, "Klub Močvara", null, 0, "20:00 - 03:00" },
                    { 8, "Savska cesta 160", 400, "+38598123456", null, "Industrijski bar s koncertima indie i rock bendova.", "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800", 45.789999999999999, 15.962999999999999, "Vintage Industrial Bar", null, 1, "19:00 - 02:00" },
                    { 9, "Radnička cesta 27", 800, "+38591222333", null, "Underground techno prostor u bivšoj tvornici.", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=800", 45.802999999999997, 16.015000000000001, "Katran", null, 0, "23:00 - 07:00" },
                    { 10, "Radnička cesta 21", 700, "+38591444555", null, "House i techno klub s vrhunskim sound systemom.", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=800", 45.804000000000002, 16.012, "Sirup Club", null, 0, "23:00 - 06:00" },
                    { 11, "Savska cesta 141", 900, "+38591666777", null, "Mainstream klub s komercijalnim hitovima i gostujućim DJ-evima.", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=800", 45.787999999999997, 15.965, "Opera Club", null, 0, "23:00 - 05:00" },
                    { 12, "Trg Krešimira Ćosića 9", 500, "+38591888999", null, "R'n'B i hip-hop klub u centru grada.", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=800", 45.798999999999999, 15.970000000000001, "Masters Club", null, 0, "22:00 - 05:00" },
                    { 13, "Tkalčićeva 41", 200, "+38591101010", null, "Popularni bar u Tkalči s DJ programom vikendom.", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=800", 45.814999999999998, 15.976000000000001, "Pločnik", null, 1, "08:00 - 24:00" },
                    { 14, "Trnjanska cesta", 1000, "+38591202020", null, "Veliki techno prostor za rave partyje.", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800", 45.789999999999999, 15.99, "Hangar", null, 0, "23:00 - 08:00" },
                    { 15, "Trnjanski nasip 23", 600, "+38591303030", null, "Prostor za alternativnu i nezavisnu kulturu.", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800", 45.792999999999999, 15.978, "Pogon Jedinstvo", null, 0, "20:00 - 04:00" },
                    { 16, "Baruna Filipovića 23a", 350, "+38516323165", null, "Prostor za umjetnost i događanja u bivšoj tvorničkoj hali.", "https://images.unsplash.com/photo-1504333638930-c8787321eee0?w=800", 45.811999999999998, 15.945, "Lauba", null, 1, "11:00 - 24:00" },
                    { 17, "Trg Krešimira Ćosića 11", 6000, "+38513650333", null, "Dvorana za velike koncerte i događanja.", "https://images.unsplash.com/photo-1540039155733-5bb30b53aa14?w=800", 45.805999999999997, 15.954000000000001, "Dom Sportova", null, 0, "po programu" },
                    { 18, "Vice Vukova 8", 15000, "+38516121111", null, "Najveća koncertna arena u Zagrebu.", "https://images.unsplash.com/photo-1429962714451-bb934ecdc4ec?w=800", 45.770000000000003, 15.943, "Arena Zagreb", null, 0, "po programu" },
                    { 19, "Trg Stjepana Radića 4", 1850, "+38516121166", null, "Koncertna dvorana za klasičnu i ozbiljnu glazbu.", "https://images.unsplash.com/photo-1465847899084-d164df4dedc6?w=800", 45.802, 15.967000000000001, "KD Vatroslav Lisinski", null, 0, "po programu" },
                    { 20, "Jezero Jarun", 50000, "+38591505050", null, "Otvoreni prostor uz jezero, domaćin velikih festivala.", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=800", 45.781999999999996, 15.914999999999999, "Jarun Plaža", null, 3, "open-air" },
                    { 21, "Maksimirski perivoj", 8000, "+38591606060", null, "Najveći zagrebački park, domaćin open-air događanja.", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=800", 45.826000000000001, 16.018000000000001, "Park Maksimir", null, 3, "open-air" },
                    { 22, "Schrottova ulica", 2000, "+38591707070", null, "Ljetna pozornica i sportski centar na Šalati.", "https://images.unsplash.com/photo-1492011221367-f47e3ccd77a0?w=800", 45.823, 15.984999999999999, "Šalata", null, 3, "open-air" },
                    { 23, "Ribnjak", 1500, "+38591808080", null, "Gradski park s ljetnim chill događanjima.", "https://images.unsplash.com/photo-1470770841072-f978cf4d019e?w=800", 45.817, 15.981, "Park Ribnjak", null, 3, "open-air" },
                    { 24, "Ilica 63", 60, "+38591909090", null, "Poznata kavana specijalizirana za vrhunsku kavu.", "https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800", 45.811, 15.965, "Eli's Caffe", null, 2, "07:00 - 20:00" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 10, 18, null, "Veliko otvorenje ljetne sezone na Jarunu uz top elektronske DJ-eve.", new DateTime(2026, 6, 21, 6, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, "Aquarius Summer Opening", "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", new DateTime(2026, 6, 20, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 11, 16, null, "Legendarni zagrebački bend uživo u Boogaloou.", new DateTime(2026, 7, 5, 1, 0, 0, 0, DateTimeKind.Unspecified), 25.00m, "Hladno Pivo Live", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 7, 4, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 12, 18, null, "Noć techno glazbe s rezidentnim i gostujućim DJ-evima.", new DateTime(2026, 6, 20, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Tvornica Techno Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 6, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 6 },
                    { 13, 16, null, "Underground punk i rock bendovi u Močvari.", new DateTime(2026, 6, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, "Punk Rock Večer", "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=600", new DateTime(2026, 6, 21, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 14, 0, null, "Akustični i indie nastupi u Vintage Industrial baru.", new DateTime(2026, 6, 18, 23, 30, 0, 0, DateTimeKind.Unspecified), 8.00m, "Indie Live Session", "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", new DateTime(2026, 6, 18, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 15, 18, null, "Cijela noć techna u industrijskom ambijentu Katrana.", new DateTime(2026, 6, 28, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, "Katran Underground: Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 6, 27, 23, 30, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 16, 18, null, "Najbolji house DJ-evi grada na jednom mjestu.", new DateTime(2026, 7, 6, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, "House Nation", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 7, 5, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 10 },
                    { 17, 18, null, "Subotnja party noć s komercijalnim hitovima.", new DateTime(2026, 6, 29, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, "Opera Saturday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 6, 28, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 11 },
                    { 18, 18, null, "R'n'B i hip-hop klasici cijelu noć.", new DateTime(2026, 7, 12, 5, 0, 0, 0, DateTimeKind.Unspecified), 14.00m, "Masters R'n'B Night", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 7, 11, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 12 },
                    { 19, 0, null, "Opušteni akustični nastup u srcu Tkalče.", new DateTime(2026, 6, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, "Pločnik Acoustic", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 6, 25, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 },
                    { 20, 18, null, "Industrijski rave s europskim techno headlinerima.", new DateTime(2026, 7, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), 22.00m, "Hangar Rave", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 12, 23, 30, 0, 0, DateTimeKind.Unspecified), 0, 14 },
                    { 21, 0, null, "Nezavisni bendovi i izvođači u Pogonu.", new DateTime(2026, 6, 27, 1, 0, 0, 0, DateTimeKind.Unspecified), 7.00m, "Alternativna Scena", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 6, 26, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 15 },
                    { 22, 18, null, "Spoj umjetničke izložbe i DJ seta u Laubi.", new DateTime(2026, 7, 4, 1, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, "Art & Beats", "https://images.unsplash.com/photo-1504333638930-c8787321eee0?w=600", new DateTime(2026, 7, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 16 },
                    { 23, 0, null, "Veliki koncert Gibonnija u Domu sportova.", new DateTime(2026, 9, 15, 23, 0, 0, 0, DateTimeKind.Unspecified), 35.00m, "Gibonni Live", "https://images.unsplash.com/photo-1540039155733-5bb30b53aa14?w=600", new DateTime(2026, 9, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 17 },
                    { 24, 0, null, "Veliki pop spektakl u Areni Zagreb.", new DateTime(2026, 9, 20, 23, 30, 0, 0, DateTimeKind.Unspecified), 40.00m, "Severina Spektakl", "https://images.unsplash.com/photo-1429962714451-bb934ecdc4ec?w=600", new DateTime(2026, 9, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 18 },
                    { 25, 0, null, "Večer klasične glazbe u dvorani Lisinski.", new DateTime(2026, 6, 30, 22, 0, 0, 0, DateTimeKind.Unspecified), 28.00m, "Zagrebačka Filharmonija", "https://images.unsplash.com/photo-1465847899084-d164df4dedc6?w=600", new DateTime(2026, 6, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 19 },
                    { 26, 16, null, "Najveći hrvatski open-air glazbeni festival na Jarunu.", new DateTime(2026, 6, 25, 2, 0, 0, 0, DateTimeKind.Unspecified), 89.00m, "INmusic Festival", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 6, 22, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, 20 },
                    { 27, 16, null, "Cjelodnevni open-air festival u parku Maksimir.", new DateTime(2026, 7, 19, 1, 0, 0, 0, DateTimeKind.Unspecified), 30.00m, "Maksimir Open Air", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 7, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, 21 },
                    { 28, 18, null, "Ljetne DJ večeri pod zvijezdama na Šalati.", new DateTime(2026, 7, 26, 3, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, "Šalata Summer Sessions", "https://images.unsplash.com/photo-1492011221367-f47e3ccd77a0?w=600", new DateTime(2026, 7, 25, 21, 0, 0, 0, DateTimeKind.Unspecified), 0, 22 },
                    { 29, 0, null, "Opuštene DJ večeri u parku Ribnjak.", new DateTime(2026, 6, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, "Ribnjak Chill Sessions", "https://images.unsplash.com/photo-1470770841072-f978cf4d019e?w=600", new DateTime(2026, 6, 17, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 23 },
                    { 30, 0, null, "Jutarnji jazz uz vrhunsku kavu u Eli's Caffeu.", new DateTime(2026, 6, 16, 13, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, "Coffee & Jazz", "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", new DateTime(2026, 6, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 24 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[,]
                {
                    { 12, 6, 1, 4, 1 },
                    { 13, 4, 2, 4, 0 },
                    { 14, 6, 1, 5, 1 },
                    { 15, 4, 2, 5, 0 },
                    { 16, 6, 1, 6, 1 },
                    { 17, 4, 2, 6, 0 },
                    { 18, 4, 1, 7, 0 },
                    { 19, 4, 2, 7, 0 },
                    { 20, 4, 1, 8, 0 },
                    { 21, 6, 2, 8, 1 },
                    { 22, 8, 1, 9, 1 },
                    { 23, 4, 2, 9, 0 },
                    { 24, 6, 1, 10, 1 },
                    { 25, 4, 2, 10, 0 },
                    { 26, 6, 1, 11, 1 },
                    { 27, 4, 2, 11, 0 },
                    { 28, 6, 1, 12, 1 },
                    { 29, 4, 2, 12, 0 },
                    { 30, 4, 1, 13, 0 },
                    { 31, 2, 2, 13, 0 },
                    { 32, 8, 1, 14, 1 },
                    { 33, 4, 2, 14, 0 },
                    { 34, 4, 1, 15, 0 },
                    { 35, 4, 2, 15, 0 },
                    { 36, 6, 1, 16, 1 },
                    { 37, 4, 2, 16, 0 },
                    { 38, 4, 1, 20, 0 },
                    { 39, 6, 2, 20, 1 },
                    { 40, 4, 1, 22, 0 },
                    { 41, 4, 2, 22, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 24);
        }
    }
}
