using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OIB = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerAppUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    EntryPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AgeLimit = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceListItems_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false),
                    Zone = table.Column<int>(type: "int", nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteVenue",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteVenue", x => new { x.UserId, x.VenueId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteVenue_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteVenue_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinimumSpending = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AppUserId", "DateOfBirth", "DeletedAt", "Email", "FirstName", "LastName", "PhoneNumber", "RegisteredAt", "Role" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2003, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ivan.golubic@email.com", "Ivan", "Golubić", "+385911234567", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, null, new DateTime(2001, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ana.horvat@email.com", "Ana", "Horvat", "+385917654321", new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, null, new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "marko.kovacevic@email.com", "Marko", "Kovačević", "+385921112233", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, null, new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "petra.babic@email.com", "Petra", "Babić", "+385998887766", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 5, null, new DateTime(1985, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "luka.peric@admin.com", "Luka", "Perić", "+385915556677", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, null, new DateTime(2010, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "karlo.novak@email.com", "Karlo", "Novak", "+385912223344", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Capacity", "ContactPhone", "DeletedAt", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "OwnerAppUserId", "Type", "WorkingHours" },
                values: new object[,]
                {
                    { 1, "Jabukovac 10, Zagreb", 500, "+38514567890", null, "Najpoznatiji noćni klub u Zagrebu s vrhunskim DJ programom.", "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800", 45.814399999999999, 15.9786, "Club Culture", null, 0, "22:00 - 05:00" },
                    { 2, "Ilica 45, Zagreb", 80, "+38511234567", null, "Ugodan kafić u centru Zagreba s pub kviz večerima.", "https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800", 45.813099999999999, 15.9665, "Kavana Lav", null, 2, "08:00 - 23:00" },
                    { 3, "Bundek, Novi Zagreb", 5000, "+38519876543", null, "Open-air pozornica pored jezera Bundek za festivale i koncerte.", "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800", 45.786900000000003, 15.987399999999999, "Park Stage Bundek", null, 3, "16:00 - 02:00" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeLimit", "DeletedAt", "Description", "EndTime", "EntryPrice", "Name", "PosterUrl", "StartTime", "Type", "VenueId" },
                values: new object[,]
                {
                    { 1, 18, null, "Najbolja techno večer u gradu s rezidentnim DJ-em MLADY koji dolazi direktno iz Berlina.", new DateTime(2026, 4, 26, 5, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Techno Night ft. MLADY", "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", new DateTime(2026, 4, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 2, 18, null, "Vojko V dolazi u Club Culture na ekskluzivni nastup! Jedna od najpopularnijih domaćih glazbenih zvezda.", new DateTime(2026, 5, 13, 3, 0, 0, 0, DateTimeKind.Unspecified), 25.00m, "Vojko V Live", "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", new DateTime(2026, 5, 12, 22, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 3, 18, null, "Vratite se u 90-te uz najbolje hitove!", new DateTime(2026, 3, 16, 4, 0, 0, 0, DateTimeKind.Unspecified), 10.00m, "Retro Party 90s", "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", new DateTime(2026, 3, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 4, 0, null, "Testiraj svoje znanje svaku srijedu! Nagrada za 1. mjesto.", new DateTime(2026, 4, 29, 22, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, "Pub Quiz Srijeda", "https://images.unsplash.com/photo-1606761568499-6d2451b23c66?w=600", new DateTime(2026, 4, 29, 19, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 5, 0, null, "Akustični nastupi lokalnih bendova uz craft pivo.", new DateTime(2026, 5, 9, 23, 0, 0, 0, DateTimeKind.Unspecified), 5.00m, "Acoustic Night - Lokalni bendovi", "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", new DateTime(2026, 5, 9, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 6, 0, null, "Specijalni pub quiz o filmovima.", new DateTime(2026, 3, 19, 22, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, "Pub Quiz - Filmska Tematika", "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=600", new DateTime(2026, 3, 19, 19, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 7, 18, null, "Dvodnevni festival elektronske glazbe na Bundeku s top europskim DJ-evima.", new DateTime(2026, 6, 22, 2, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, "Zagreb Summer Beats", "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", new DateTime(2026, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 8, 0, null, "Legendarni Let 3 na pozornici Bundek!", new DateTime(2026, 5, 10, 23, 30, 0, 0, DateTimeKind.Unspecified), 20.00m, "Let 3 - Bundek Open Air", "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=600", new DateTime(2026, 5, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 9, 16, null, "Proljetni mini-festival s lokalnim DJ-evima.", new DateTime(2026, 3, 22, 23, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, "Spring Vibes Festival", "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", new DateTime(2026, 3, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PriceListItems",
                columns: new[] { "Id", "Category", "ItemName", "Price", "VenueId" },
                values: new object[,]
                {
                    { 1, "Piće", "Gin & Tonic", 8.00m, 1 },
                    { 2, "Piće", "Vodka Red Bull", 9.00m, 1 },
                    { 3, "Piće", "Jack & Coke", 10.00m, 1 },
                    { 4, "Piće", "Heineken 0.5l", 5.00m, 1 },
                    { 5, "Ulaznica", "VIP ulaz", 30.00m, 1 },
                    { 6, "Piće", "Espresso", 1.50m, 2 },
                    { 7, "Piće", "Cappuccino", 2.50m, 2 },
                    { 8, "Piće", "Craft pivo", 5.00m, 2 },
                    { 9, "Hrana", "Sendvič", 4.50m, 2 },
                    { 10, "Piće", "Pivo 0.5l", 4.00m, 3 },
                    { 11, "Piće", "Kokteli", 7.00m, 3 },
                    { 12, "Hrana", "Pizza komad", 3.50m, 3 },
                    { 13, "Ulaznica", "Festival pass", 50.00m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "SeatCount", "TableNumber", "VenueId", "Zone" },
                values: new object[,]
                {
                    { 1, 4, 1, 1, 0 },
                    { 2, 6, 2, 1, 1 },
                    { 3, 8, 3, 1, 1 },
                    { 4, 4, 4, 1, 0 },
                    { 5, 4, 1, 2, 0 },
                    { 6, 6, 2, 2, 0 },
                    { 7, 4, 3, 2, 0 },
                    { 8, 6, 1, 3, 1 },
                    { 9, 6, 2, 3, 1 },
                    { 10, 4, 3, 3, 0 },
                    { 11, 4, 4, 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserFavoriteVenue",
                columns: new[] { "UserId", "VenueId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 4, 1 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CreatedAt", "EventId", "MinimumSpending", "Note", "NumberOfGuests", "Status", "TableId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 100.00m, "Rođendan, molimo balon dekoraciju", 4, 1, 2, 1 },
                    { 2, new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 150.00m, "", 6, 0, 3, 2 },
                    { 3, new DateTime(2026, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 0.00m, "Blizu pozornice ako je moguće", 3, 1, 5, 4 },
                    { 4, new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 200.00m, "Otkazano zbog bolesti", 5, 2, 8, 1 },
                    { 5, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 0.00m, "", 4, 1, 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "EventId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "Odlična atmosfera, DJ je bio fenomenalan!", new DateTime(2026, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 1 },
                    { 2, "Super quiz, pitanja su bila zanimljiva.", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 2 },
                    { 3, "Bilo je OK, ali predugo čekanje za piće.", new DateTime(2026, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 4 },
                    { 4, "Najbolji festival ove godine, 10/10!", new DateTime(2026, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 5, 1 },
                    { 5, "Dobar vibe, lokacija predivna.", new DateTime(2026, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItems_VenueId",
                table: "PriceListItems",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventId",
                table: "Reservations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EventId",
                table: "Reviews",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_VenueId",
                table: "Tables",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteVenue_VenueId",
                table: "UserFavoriteVenue",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PriceListItems");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserFavoriteVenue");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
