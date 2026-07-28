using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EventsJulyAugust2026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 200, 0, null, "Tjedni kviz znanja na špici — ekipe do 6 igrača, prijava na šanku.", new DateTime(2026, 7, 28, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 7, 28, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 201, 0, null, "Zalazak sunca i lagani house na krovu Ilice 16.", new DateTime(2026, 7, 29, 1, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, false, "Rooftop Sunset Session", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 7, 28, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 202, 0, null, "Rock klasici i živa svirka do kasno.", new DateTime(2026, 7, 30, 2, 0, 0, 0, DateTimeKind.Unspecified), 7.00m, false, "Alkatraz Rock Wednesday", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 7, 29, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 104 },
                    { 203, 0, null, "Indie i alternativa uz domaće bendove.", new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Vintage Indie Night", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 7, 29, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 204, 18, null, "Rezidentni DJ-evi i gost iz Berlina u industrijskom ambijentu.", new DateTime(2026, 7, 31, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, true, "Katran: Warehouse Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 7, 30, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 205, 18, null, "R'n'B i hip-hop selekcija uz koktele.", new DateTime(2026, 7, 31, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "H2O Thursday Vibes", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 7, 30, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 206, 0, null, "Tri benda, jedna večer — showcase domaće scene.", new DateTime(2026, 7, 31, 1, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Boogaloo Live: Domaći Bendovi", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 7, 30, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 207, 18, null, "Petak u H2O — komercijalni hitovi i VIP zona.", new DateTime(2026, 8, 1, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, true, "H2O Friday Pure Fun", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 208, 18, null, "Balkan i pop hitovi do jutra.", new DateTime(2026, 8, 1, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "EX Balkan Night", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 26 },
                    { 209, 18, null, "Glamurozna noć uz rezidentne DJ-eve.", new DateTime(2026, 8, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Friday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 210, 18, null, "House u vrtu kluba Mint.", new DateTime(2026, 8, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Garden Party", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 211, 18, null, "Domaća zabava i hitovi u Club & Lounge Roko.", new DateTime(2026, 8, 1, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Fešta", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 },
                    { 212, 18, null, "House i pop hitovi u Vlaškoj.", new DateTime(2026, 8, 1, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "XO Friday Party", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 101 },
                    { 213, 18, null, "Otvorenje vikenda u Roccu.", new DateTime(2026, 8, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Rocco Weekend Opening", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 103 },
                    { 214, 18, null, "Ljetni rave s europskim techno headlinerima.", new DateTime(2026, 8, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Hangar Rave: Summer Edition", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 14 },
                    { 215, 18, null, "Vikend party u Sovi — do zore.", new DateTime(2026, 8, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Night Fever", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 31, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 216, 18, null, "Petak koji se ne priča dalje.", new DateTime(2026, 8, 1, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Secret Friday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 108 },
                    { 217, 18, null, "Underground techno u bunkeru ispod Ilice.", new DateTime(2026, 8, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Bunker Techno Session", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 7, 31, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 106 },
                    { 218, 18, null, "Slijedi osjećaj — domaći i strani hitovi.", new DateTime(2026, 8, 1, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Osjećaj Petak", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 7, 31, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 219, 18, null, "Cijela noć techna — bez pauze do jutra.", new DateTime(2026, 8, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, true, "Katran All Night Long", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 220, 18, null, "Subotnja party noć s komercijalnim hitovima.", new DateTime(2026, 8, 2, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Opera Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 11 },
                    { 221, 18, null, "R'n'B i hip-hop klasici cijelu noć.", new DateTime(2026, 8, 2, 5, 0, 0, 0, DateTimeKind.Unspecified), 14.00m, false, "Masters R'n'B Night", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 8, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 12 },
                    { 222, 18, null, "Elegantna subota u samom centru.", new DateTime(2026, 8, 2, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Saturday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 223, 18, null, "Ljetna noć elektronske glazbe na Jarunu.", new DateTime(2026, 8, 2, 6, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Aquarius Summer Night", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 224, 0, null, "Cjelodnevni open-air uz jezero — 8 DJ-eva, food trucks.", new DateTime(2026, 8, 2, 4, 0, 0, 0, DateTimeKind.Unspecified), 25.00m, true, "Jarun Beach Open Air", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 8, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, 20 },
                    { 225, 18, null, "Subota u Ritzu uz rezidente.", new DateTime(2026, 8, 2, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 226, 0, null, "Nedjeljni chill u parku uz lagane ritmove.", new DateTime(2026, 8, 2, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Ribnjak Chill Sunday", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 8, 2, 18, 0, 0, 0, DateTimeKind.Unspecified), 0, 23 },
                    { 227, 0, null, "Nedjeljni zalazak na krovu.", new DateTime(2026, 8, 2, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Rooftop Sunday Sunset", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 8, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 228, 0, null, "Tjedni kviz znanja — nova runda pitanja.", new DateTime(2026, 8, 3, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 8, 3, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 229, 0, null, "Lagani beat i pogled na krovove Zagreba.", new DateTime(2026, 8, 5, 1, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, false, "Rooftop Tuesday Sessions", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 8, 4, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 230, 0, null, "Alternativna i underground scena.", new DateTime(2026, 8, 6, 1, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Močvara Alternative Night", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 8, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 231, 0, null, "Živa rock svirka i glasne gitare.", new DateTime(2026, 8, 6, 2, 0, 0, 0, DateTimeKind.Unspecified), 7.00m, false, "Alkatraz Rock Night", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 8, 5, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 104 },
                    { 232, 0, null, "Koncertna večer u Tvornici Kulture.", new DateTime(2026, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, false, "Tvornica Live: Ljetni Koncert", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 8, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 233, 18, null, "Rezidentna techno večer.", new DateTime(2026, 8, 7, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Katran Underground: Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 8, 6, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 234, 18, null, "R'n'B i hip-hop selekcija uz koktele.", new DateTime(2026, 8, 7, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "H2O Thursday Vibes", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 8, 6, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 235, 18, null, "Petak u H2O — komercijalni hitovi i VIP zona.", new DateTime(2026, 8, 8, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "H2O Friday Pure Fun", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 236, 18, null, "Balkan i pop program.", new DateTime(2026, 8, 8, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "EX Friday Balkan", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 26 },
                    { 237, 18, null, "Glamurozni petak uz rezidentne DJ-eve.", new DateTime(2026, 8, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Friday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 238, 18, null, "House i techno u vrtu kluba Mint.", new DateTime(2026, 8, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Garden Sessions", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 239, 18, null, "Petak u XO klubu.", new DateTime(2026, 8, 8, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "XO Friday Party", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 101 },
                    { 240, 18, null, "Industrijski rave do jutra.", new DateTime(2026, 8, 8, 7, 0, 0, 0, DateTimeKind.Unspecified), 22.00m, true, "Hangar Warehouse Rave", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 14 },
                    { 241, 18, null, "Skriveni petak za one koji znaju.", new DateTime(2026, 8, 8, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Secret Friday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 108 },
                    { 242, 18, null, "Deep i techno u bunkeru.", new DateTime(2026, 8, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Bunker Deep Session", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 106 },
                    { 243, 18, null, "Vikend zabava u Roccu.", new DateTime(2026, 8, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Rocco Friday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 103 },
                    { 244, 18, null, "Vikend u Sovi — do zore.", new DateTime(2026, 8, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Weekend Party", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 7, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 245, 18, null, "Domaći hitovi i zabava.", new DateTime(2026, 8, 8, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Vikend Fešta", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 },
                    { 246, 18, null, "Subotnja elektronska noć na Jarunu.", new DateTime(2026, 8, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Aquarius Saturday Sessions", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 247, 18, null, "Techno maraton do jutra.", new DateTime(2026, 8, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, false, "Katran All Night Long", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 8, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 248, 18, null, "Komercijalni hitovi i gostujući DJ.", new DateTime(2026, 8, 9, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Opera Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 8, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 11 },
                    { 249, 18, null, "Hip-hop i trap selekcija.", new DateTime(2026, 8, 9, 5, 0, 0, 0, DateTimeKind.Unspecified), 14.00m, false, "Masters Hip-Hop Night", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 8, 8, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 12 },
                    { 250, 18, null, "Subota u THE Clubu.", new DateTime(2026, 8, 9, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Saturday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 8, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 251, 18, null, "Ljetne DJ večeri pod zvijezdama.", new DateTime(2026, 8, 9, 3, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Šalata Summer Sessions", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 8, 8, 21, 0, 0, 0, DateTimeKind.Unspecified), 0, 22 },
                    { 252, 18, null, "Subota u vrtu Minta.", new DateTime(2026, 8, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Saturday Garden", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 8, 8, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 253, 0, null, "Nedjeljni open-air u parku — besplatan ulaz.", new DateTime(2026, 8, 9, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, true, "Maksimir Open Air Sunday", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 8, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, 21 },
                    { 254, 0, null, "Nedjeljni chill uz jezero.", new DateTime(2026, 8, 9, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Jarun Sunset Chill", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 8, 9, 18, 0, 0, 0, DateTimeKind.Unspecified), 0, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 254);
        }
    }
}
