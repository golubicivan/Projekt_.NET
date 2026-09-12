using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EventsSeptember2026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "IsFeatured", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 300, 18, null, "Otvorenje sezone u H2O — rezidenti i gostujući DJ do jutra.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, true, "H2O Season Opening", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 301, 18, null, "Povratak sezone u Katran — techno maraton u industrijskom ambijentu.", new DateTime(2026, 9, 13, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, true, "Katran: Season Opening Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 302, 18, null, "Subota u Ritzu uz rezidentne DJ-eve.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 303, 18, null, "House i techno u vrtu kluba Mint.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Saturday Garden", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 304, 18, null, "Elegantna subota u samom centru.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Saturday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 305, 18, null, "Subotnja party noć s komercijalnim hitovima.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Opera Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 11 },
                    { 306, 18, null, "R'n'B i hip-hop klasici cijelu noć.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 14.00m, false, "Masters R'n'B Night", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 12 },
                    { 307, 18, null, "Subotnja zabava u Roccu.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Rocco Saturday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 103 },
                    { 308, 18, null, "Vikend party u Sovi — do zore.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Weekend Party", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 309, 18, null, "Subota koja se ne priča dalje.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Secret Saturday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 108 },
                    { 310, 18, null, "Underground techno u bunkeru ispod Ilice.", new DateTime(2026, 9, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Bunker Techno Session", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 106 },
                    { 311, 18, null, "Subota u XO klubu uz house i pop.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "XO Saturday Party", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 101 },
                    { 312, 18, null, "Balkan i pop hitovi do jutra.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "EX Balkan Night", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 26 },
                    { 313, 18, null, "Domaća zabava i hitovi u Club & Lounge Roko.", new DateTime(2026, 9, 13, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Fešta", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 12, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 },
                    { 314, 0, null, "Zalazak sunca i lagani house na krovu Ilice 16.", new DateTime(2026, 9, 13, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Rooftop Sunset Session", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 9, 13, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 315, 0, null, "Nedjeljni jam session — otvoreni mikrofon za bendove.", new DateTime(2026, 9, 13, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Vintage Sunday Jam", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 13, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 316, 0, null, "Norveški kantautor i frontmen Madrugade uživo u Boogaloou.", new DateTime(2026, 9, 14, 23, 0, 0, 0, DateTimeKind.Unspecified), 25.00m, true, "Sivert Høyem", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 14, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 317, 0, null, "Tjedni kviz znanja na špici — ekipe do 6 igrača.", new DateTime(2026, 9, 14, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 9, 14, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 318, 0, null, "Lagani beat i pogled na krovove Zagreba.", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, false, "Rooftop Tuesday Sessions", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 9, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 319, 0, null, "Rock klasici i živa svirka do kasno.", new DateTime(2026, 9, 17, 2, 0, 0, 0, DateTimeKind.Unspecified), 7.00m, false, "Alkatraz Rock Wednesday", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 9, 16, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 104 },
                    { 320, 0, null, "Alternativna i underground scena uz Savu.", new DateTime(2026, 9, 17, 1, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Močvara Alternative Night", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 9, 16, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 321, 18, null, "Rezidentna techno večer.", new DateTime(2026, 9, 18, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Katran Thursday Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 322, 18, null, "R'n'B i hip-hop selekcija uz koktele.", new DateTime(2026, 9, 18, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "H2O Thursday Vibes", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 17, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 323, 18, null, "Slijedi osjećaj — domaći i strani hitovi.", new DateTime(2026, 9, 18, 4, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Osjećaj Četvrtak", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 17, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 324, 0, null, "Britanski soul/electronica autor uživo u Boogaloou.", new DateTime(2026, 9, 19, 1, 0, 0, 0, DateTimeKind.Unspecified), 22.00m, true, "Jamie Woon", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 18, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 325, 18, null, "Petak u H2O — komercijalni hitovi i VIP zona.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "H2O Friday Pure Fun", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 326, 18, null, "Glamurozna noć uz rezidentne DJ-eve.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Friday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 327, 18, null, "House u vrtu kluba Mint.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Friday Garden", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 328, 18, null, "Balkan i pop program.", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "EX Friday Balkan", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 26 },
                    { 329, 18, null, "Industrijski rave s europskim techno headlinerima.", new DateTime(2026, 9, 19, 7, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Hangar Rave", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 14 },
                    { 330, 18, null, "Petak u XO klubu.", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "XO Friday Party", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 101 },
                    { 331, 18, null, "Vikend zabava u Roccu.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Rocco Friday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 103 },
                    { 332, 18, null, "Skriveni petak za one koji znaju.", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Secret Friday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 108 },
                    { 333, 18, null, "Deep i techno u bunkeru.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Bunker Deep Session", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 106 },
                    { 334, 18, null, "Vikend u Sovi — do zore.", new DateTime(2026, 9, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Night Fever", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 335, 18, null, "Domaći hitovi i zabava.", new DateTime(2026, 9, 19, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Roko Vikend Fešta", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 31 },
                    { 336, 0, null, "Srpski rock sastav Van Gogh uz podršku benda Grad.", new DateTime(2026, 9, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Van Gogh + Grad", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 9, 19, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 337, 18, null, "Cijela noć techna — bez pauze do jutra.", new DateTime(2026, 9, 20, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, false, "Katran All Night Long", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 338, 18, null, "Subota u H2O uz rezidente.", new DateTime(2026, 9, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "H2O Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 339, 18, null, "Otvorenje sezone na Jarunu — elektronska noć.", new DateTime(2026, 9, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, true, "Aquarius Season Opening", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 340, 18, null, "Subota u Ritzu.", new DateTime(2026, 9, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 341, 18, null, "Subota u vrtu Minta.", new DateTime(2026, 9, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Saturday Garden", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 342, 18, null, "Subota u THE Clubu.", new DateTime(2026, 9, 20, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Saturday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 343, 18, null, "Komercijalni hitovi i gostujući DJ.", new DateTime(2026, 9, 20, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Opera Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 11 },
                    { 344, 18, null, "Hip-hop i trap selekcija.", new DateTime(2026, 9, 20, 5, 0, 0, 0, DateTimeKind.Unspecified), 14.00m, false, "Masters Hip-Hop Night", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 19, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 12 },
                    { 345, 0, null, "Živa rock svirka i glasne gitare.", new DateTime(2026, 9, 20, 2, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Alkatraz Rock Night", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 9, 19, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 104 },
                    { 346, 0, null, "Koncertna večer u Tvornici Kulture.", new DateTime(2026, 9, 20, 23, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Lavina i Alexandra Căpitănescu", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 347, 0, null, "Nedjeljni chill u parku uz lagane ritmove.", new DateTime(2026, 9, 20, 22, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Ribnjak Chill Sunday", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 9, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), 0, 23 },
                    { 348, 0, null, "Tjedni kviz znanja — nova runda pitanja.", new DateTime(2026, 9, 21, 22, 30, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Bulldog Pub Quiz", "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", new DateTime(2026, 9, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 105 },
                    { 349, 0, null, "Lagani beat i pogled na krovove.", new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, false, "Rooftop Tuesday Sessions", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 9, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 350, 0, null, "Indie i alternativa uz domaće bendove.", new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Vintage Indie Night", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 23, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 351, 0, null, "Punk i hardcore večer.", new DateTime(2026, 9, 24, 1, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Močvara Punk Wednesday", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 9, 23, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 352, 18, null, "Američki industrial metal veterani uživo u Boogaloou.", new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.00m, true, "Fear Factory", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", new DateTime(2026, 9, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 353, 18, null, "Rezidenti i gost u podrumu Katrana.", new DateTime(2026, 9, 25, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Katran Underground: Techno", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 354, 18, null, "R'n'B i hip-hop selekcija.", new DateTime(2026, 9, 25, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "H2O Thursday Vibes", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 355, 18, null, "Petak u H2O.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "H2O Friday Pure Fun", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 356, 18, null, "Glamurozni petak.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Glamour Friday", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 357, 18, null, "House u vrtu Minta.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Friday Garden", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 358, 18, null, "Balkan i pop program.", new DateTime(2026, 9, 26, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "EX Friday Balkan", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 26 },
                    { 359, 18, null, "Industrijski rave do jutra.", new DateTime(2026, 9, 26, 7, 0, 0, 0, DateTimeKind.Unspecified), 22.00m, true, "Hangar Warehouse Rave", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 14 },
                    { 360, 18, null, "Petak u XO klubu.", new DateTime(2026, 9, 26, 5, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "XO Friday Party", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 101 },
                    { 361, 18, null, "Vikend u Roccu.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Rocco Friday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 103 },
                    { 362, 18, null, "Petak koji se ne priča dalje.", new DateTime(2026, 9, 26, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Secret Friday", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 108 },
                    { 363, 18, null, "Techno u bunkeru.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Bunker Techno Session", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 106 },
                    { 364, 18, null, "Vikend u Sovi.", new DateTime(2026, 9, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), 8.00m, false, "Sova Weekend Party", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 25, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 102 },
                    { 365, 18, null, "Domaći i strani hitovi.", new DateTime(2026, 9, 26, 4, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, false, "Osjećaj Petak", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 25, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 27 },
                    { 366, 18, null, "Domaći hip-hop u Tvornici Kulture.", new DateTime(2026, 9, 27, 1, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, true, "Lil Drito iz Tvornice", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 26, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 367, 18, null, "Techno maraton do jutra.", new DateTime(2026, 9, 27, 7, 0, 0, 0, DateTimeKind.Unspecified), 18.00m, false, "Katran All Night Long", "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 9 },
                    { 368, 18, null, "Subota u H2O.", new DateTime(2026, 9, 27, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "H2O Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 25 },
                    { 369, 18, null, "Subotnja elektronska noć na Jarunu.", new DateTime(2026, 9, 27, 6, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, false, "Aquarius Saturday Sessions", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 370, 18, null, "Subota u THE Clubu.", new DateTime(2026, 9, 27, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "THE Saturday", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 29 },
                    { 371, 18, null, "Subotnji program.", new DateTime(2026, 9, 27, 5, 0, 0, 0, DateTimeKind.Unspecified), 12.00m, false, "Opera Saturday", "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 11 },
                    { 372, 18, null, "R'n'B klasici.", new DateTime(2026, 9, 27, 5, 0, 0, 0, DateTimeKind.Unspecified), 14.00m, false, "Masters R'n'B Night", "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", new DateTime(2026, 9, 26, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 12 },
                    { 373, 18, null, "Subota u Ritzu.", new DateTime(2026, 9, 27, 6, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, "Ritz Saturday Night", "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 28 },
                    { 374, 18, null, "Subota u vrtu Minta.", new DateTime(2026, 9, 27, 6, 0, 0, 0, DateTimeKind.Unspecified), 16.00m, false, "Mint Saturday Garden", "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", new DateTime(2026, 9, 26, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 30 },
                    { 375, 0, null, "Zadnji zalasci sezone na krovu.", new DateTime(2026, 9, 27, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Rooftop Sunset Session", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 9, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, 107 },
                    { 376, 0, null, "Nedjeljni jam session.", new DateTime(2026, 9, 27, 23, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, false, "Vintage Sunday Jam", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 9, 27, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 320);

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
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 376);
        }
    }
}
