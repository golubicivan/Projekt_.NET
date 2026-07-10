using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.Model;

namespace ZagrebEvents.DAL
{
    // Nasljeđuje IdentityDbContext kako bi ASP.NET Core Identity tablice
    // (AspNetUsers, AspNetRoles, ...) bile dio iste baze.
    public class ZagrebEventsDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public ZagrebEventsDbContext(DbContextOptions<ZagrebEventsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Venue> Venues { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<PriceListItem> PriceListItems { get; set; } = null!;
        public DbSet<Attachment> Attachments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====== N-N veza User <-> Venue (Favorites) ======
            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteVenues)
                .WithMany(v => v.FavoritedByUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFavoriteVenue",
                    j => j
                        .HasOne<Venue>().WithMany().HasForeignKey("VenueId").OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("UserId", "VenueId");
                        j.HasData(
                            new { UserId = 1, VenueId = 1 },
                            new { UserId = 1, VenueId = 3 },
                            new { UserId = 2, VenueId = 2 },
                            new { UserId = 4, VenueId = 2 },
                            new { UserId = 4, VenueId = 1 }
                        );
                    });

            // Sprijeci visestruke cascade pathove (Reservation ima 3 FK-ja koje bi cascade brisale)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Reviews)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // ====== SEED PODACI ======

            // KORISNICI (domenski profili). Lozinke su sad u Identity (AppUser), ne ovdje.
            // AppUserId se popunjava runtime seederom kad se kreiraju Identity nalozi.
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Ivan", LastName = "Golubić", DateOfBirth = new DateTime(2003, 5, 15), Email = "ivan.golubic@email.com", PhoneNumber = "+385911234567", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 1, 10) },
                new User { Id = 2, FirstName = "Ana", LastName = "Horvat", DateOfBirth = new DateTime(2001, 8, 22), Email = "ana.horvat@email.com", PhoneNumber = "+385917654321", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 2, 5) },
                new User { Id = 3, FirstName = "Marko", LastName = "Kovačević", DateOfBirth = new DateTime(1990, 3, 10), Email = "marko.kovacevic@email.com", PhoneNumber = "+385921112233", Role = UserRole.Owner, RegisteredAt = new DateTime(2025, 11, 1) },
                new User { Id = 4, FirstName = "Petra", LastName = "Babić", DateOfBirth = new DateTime(2000, 12, 1), Email = "petra.babic@email.com", PhoneNumber = "+385998887766", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 3, 1) },
                new User { Id = 5, FirstName = "Luka", LastName = "Perić", DateOfBirth = new DateTime(1985, 7, 20), Email = "luka.peric@admin.com", PhoneNumber = "+385915556677", Role = UserRole.Admin, RegisteredAt = new DateTime(2025, 6, 1) },
                new User { Id = 6, FirstName = "Karlo", LastName = "Novak", DateOfBirth = new DateTime(2010, 9, 25), Email = "karlo.novak@email.com", PhoneNumber = "+385912223344", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 3, 20) }
            );

            // VENUES
            modelBuilder.Entity<Venue>().HasData(
                new Venue { Id = 1, Name = "Club Culture", Address = "Jabukovac 10, Zagreb", Latitude = 45.8144, Longitude = 15.9786, Capacity = 500, WorkingHours = "22:00 - 05:00", ContactPhone = "+38514567890", Description = "Najpoznatiji noćni klub u Zagrebu s vrhunskim DJ programom.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800", LogoUrl = "/img/logos/culture.svg", DeletedAt = new DateTime(2026, 6, 1) },
                new Venue { Id = 2, Name = "Kavana Lav", Address = "Ilica 45, Zagreb", Latitude = 45.8131, Longitude = 15.9665, Capacity = 80, WorkingHours = "08:00 - 23:00", ContactPhone = "+38511234567", Description = "Ugodan kafić u centru Zagreba s pub kviz večerima.", Type = VenueType.Cafe, ImageUrl = "https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800", LogoUrl = "/img/logos/kavanalav.svg", DeletedAt = new DateTime(2026, 6, 1) },
                new Venue { Id = 3, Name = "Park Stage Bundek", Address = "Bundek, Novi Zagreb", Latitude = 45.7869, Longitude = 15.9874, Capacity = 5000, WorkingHours = "16:00 - 02:00", ContactPhone = "+38519876543", Description = "Open-air pozornica pored jezera Bundek za festivale i koncerte.", Type = VenueType.OpenAir, ImageUrl = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800", LogoUrl = "/img/logos/bundek.svg" }
            );

            // STOLOVI
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, VenueId = 1 },
                new Table { Id = 2, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, VenueId = 1 },
                new Table { Id = 3, TableNumber = 3, SeatCount = 8, Zone = TableZone.VIP, VenueId = 1 },
                new Table { Id = 4, TableNumber = 4, SeatCount = 4, Zone = TableZone.Regular, VenueId = 1 },
                new Table { Id = 5, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, VenueId = 2 },
                new Table { Id = 6, TableNumber = 2, SeatCount = 6, Zone = TableZone.Regular, VenueId = 2 },
                new Table { Id = 7, TableNumber = 3, SeatCount = 4, Zone = TableZone.Regular, VenueId = 2 },
                new Table { Id = 8, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 3 },
                new Table { Id = 9, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, VenueId = 3 },
                new Table { Id = 10, TableNumber = 3, SeatCount = 4, Zone = TableZone.VIP, VenueId = 3 },
                new Table { Id = 11, TableNumber = 4, SeatCount = 4, Zone = TableZone.VIP, VenueId = 3 }
            );

            // CJENIK
            modelBuilder.Entity<PriceListItem>().HasData(
                new PriceListItem { Id = 1, ItemName = "Gin & Tonic", Price = 8.00m, Category = "Piće", VenueId = 1 },
                new PriceListItem { Id = 2, ItemName = "Vodka Red Bull", Price = 9.00m, Category = "Piće", VenueId = 1 },
                new PriceListItem { Id = 3, ItemName = "Jack & Coke", Price = 10.00m, Category = "Piće", VenueId = 1 },
                new PriceListItem { Id = 4, ItemName = "Heineken 0.5l", Price = 5.00m, Category = "Piće", VenueId = 1 },
                new PriceListItem { Id = 5, ItemName = "VIP ulaz", Price = 30.00m, Category = "Ulaznica", VenueId = 1 },
                new PriceListItem { Id = 6, ItemName = "Espresso", Price = 1.50m, Category = "Piće", VenueId = 2 },
                new PriceListItem { Id = 7, ItemName = "Cappuccino", Price = 2.50m, Category = "Piće", VenueId = 2 },
                new PriceListItem { Id = 8, ItemName = "Craft pivo", Price = 5.00m, Category = "Piće", VenueId = 2 },
                new PriceListItem { Id = 9, ItemName = "Sendvič", Price = 4.50m, Category = "Hrana", VenueId = 2 },
                new PriceListItem { Id = 10, ItemName = "Pivo 0.5l", Price = 4.00m, Category = "Piće", VenueId = 3 },
                new PriceListItem { Id = 11, ItemName = "Kokteli", Price = 7.00m, Category = "Piće", VenueId = 3 },
                new PriceListItem { Id = 12, ItemName = "Pizza komad", Price = 3.50m, Category = "Hrana", VenueId = 3 },
                new PriceListItem { Id = 13, ItemName = "Festival pass", Price = 50.00m, Category = "Ulaznica", VenueId = 3 }
            );

            // EVENTI
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Name = "Techno Night ft. MLADY", Description = "Najbolja techno večer u gradu s rezidentnim DJ-em MLADY koji dolazi direktno iz Berlina.", StartTime = new DateTime(2026, 4, 25, 23, 0, 0), EndTime = new DateTime(2026, 4, 26, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 15.00m, PosterUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", AgeLimit = 18, VenueId = 1, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 2, Name = "Vojko V Live", Description = "Vojko V dolazi u Club Culture na ekskluzivni nastup! Jedna od najpopularnijih domaćih glazbenih zvezda.", StartTime = new DateTime(2026, 5, 12, 22, 0, 0), EndTime = new DateTime(2026, 5, 13, 3, 0, 0), Type = EventType.Concert, EntryPrice = 25.00m, PosterUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", AgeLimit = 18, VenueId = 1, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 3, Name = "Retro Party 90s", Description = "Vratite se u 90-te uz najbolje hitove!", StartTime = new DateTime(2026, 3, 15, 22, 0, 0), EndTime = new DateTime(2026, 3, 16, 4, 0, 0), Type = EventType.DJNight, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", AgeLimit = 18, VenueId = 1, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 4, Name = "Pub Quiz Srijeda", Description = "Testiraj svoje znanje svaku srijedu! Nagrada za 1. mjesto.", StartTime = new DateTime(2026, 4, 29, 19, 0, 0), EndTime = new DateTime(2026, 4, 29, 22, 0, 0), Type = EventType.PubQuiz, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1606761568499-6d2451b23c66?w=600", AgeLimit = 0, VenueId = 2, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 5, Name = "Acoustic Night - Lokalni bendovi", Description = "Akustični nastupi lokalnih bendova uz craft pivo.", StartTime = new DateTime(2026, 5, 9, 20, 0, 0), EndTime = new DateTime(2026, 5, 9, 23, 0, 0), Type = EventType.Concert, EntryPrice = 5.00m, PosterUrl = "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", AgeLimit = 0, VenueId = 2, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 6, Name = "Pub Quiz - Filmska Tematika", Description = "Specijalni pub quiz o filmovima.", StartTime = new DateTime(2026, 3, 19, 19, 0, 0), EndTime = new DateTime(2026, 3, 19, 22, 0, 0), Type = EventType.PubQuiz, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=600", AgeLimit = 0, VenueId = 2, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 7, Name = "Zagreb Summer Beats", Description = "Dvodnevni festival elektronske glazbe na Bundeku s top europskim DJ-evima.", StartTime = new DateTime(2026, 6, 20, 16, 0, 0), EndTime = new DateTime(2026, 6, 22, 2, 0, 0), Type = EventType.Festival, EntryPrice = 50.00m, PosterUrl = "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", AgeLimit = 18, VenueId = 3 },
                new Event { Id = 8, Name = "Let 3 - Bundek Open Air", Description = "Legendarni Let 3 na pozornici Bundek!", StartTime = new DateTime(2026, 5, 10, 20, 0, 0), EndTime = new DateTime(2026, 5, 10, 23, 30, 0), Type = EventType.Concert, EntryPrice = 20.00m, PosterUrl = "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=600", AgeLimit = 0, VenueId = 3 },
                new Event { Id = 9, Name = "Spring Vibes Festival", Description = "Proljetni mini-festival s lokalnim DJ-evima.", StartTime = new DateTime(2026, 3, 22, 17, 0, 0), EndTime = new DateTime(2026, 3, 22, 23, 0, 0), Type = EventType.Festival, EntryPrice = 15.00m, PosterUrl = "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", AgeLimit = 16, VenueId = 3 }
            );

            // REZERVACIJE
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, CreatedAt = new DateTime(2026, 3, 28), NumberOfGuests = 4, Status = ReservationStatus.Confirmed, Note = "Rođendan, molimo balon dekoraciju", MinimumSpending = 100.00m, UserId = 1, TableId = 2, EventId = 1 },
                new Reservation { Id = 2, CreatedAt = new DateTime(2026, 3, 29), NumberOfGuests = 6, Status = ReservationStatus.Pending, Note = "", MinimumSpending = 150.00m, UserId = 2, TableId = 3, EventId = 2 },
                new Reservation { Id = 3, CreatedAt = new DateTime(2026, 3, 30), NumberOfGuests = 3, Status = ReservationStatus.Confirmed, Note = "Blizu pozornice ako je moguće", MinimumSpending = 0.00m, UserId = 4, TableId = 5, EventId = 4 },
                new Reservation { Id = 4, CreatedAt = new DateTime(2026, 3, 25), NumberOfGuests = 5, Status = ReservationStatus.Cancelled, Note = "Otkazano zbog bolesti", MinimumSpending = 200.00m, UserId = 1, TableId = 8, EventId = 7 },
                new Reservation { Id = 5, CreatedAt = new DateTime(2026, 3, 31), NumberOfGuests = 4, Status = ReservationStatus.Confirmed, Note = "", MinimumSpending = 0.00m, UserId = 2, TableId = 10, EventId = 8 }
            );

            // RECENZIJE
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, Rating = 5, Comment = "Odlična atmosfera, DJ je bio fenomenalan!", CreatedAt = new DateTime(2026, 3, 16), UserId = 1, EventId = 3 },
                new Review { Id = 2, Rating = 4, Comment = "Super quiz, pitanja su bila zanimljiva.", CreatedAt = new DateTime(2026, 3, 20), UserId = 2, EventId = 6 },
                new Review { Id = 3, Rating = 3, Comment = "Bilo je OK, ali predugo čekanje za piće.", CreatedAt = new DateTime(2026, 3, 16), UserId = 4, EventId = 3 },
                new Review { Id = 4, Rating = 5, Comment = "Najbolji festival ove godine, 10/10!", CreatedAt = new DateTime(2026, 3, 23), UserId = 1, EventId = 9 },
                new Review { Id = 5, Rating = 4, Comment = "Dobar vibe, lokacija predivna.", CreatedAt = new DateTime(2026, 3, 23), UserId = 3, EventId = 9 }
            );

            // ====== STVARNE ZAGREBAČKE LOKACIJE (klubovi, barovi, kafići, parkovi) ======
            modelBuilder.Entity<Venue>().HasData(
                new Venue { Id = 4, Name = "Aquarius", Address = "Aleja Matije Ljubeka 2a, Jarun", Latitude = 45.7794, Longitude = 15.9217, Capacity = 1500, WorkingHours = "23:00 - 06:00", ContactPhone = "+38513640231", Description = "Legendarni klub na jezeru Jarun, dom elektronske glazbe u Zagrebu.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=800", LogoUrl = "/img/logos/aquarius-emblem.jpg" },
                new Venue { Id = 5, Name = "Boogaloo", Address = "Ulica grada Vukovara 68", Latitude = 45.79862, Longitude = 15.97098, Capacity = 1200, WorkingHours = "21:00 - 04:00", ContactPhone = "+38516313021", Description = "Klub i koncertni prostor za domaće i strane izvođače.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800", LogoUrl = "/img/logos/boogaloo-emblem.jpg", InstagramUrl = "https://www.instagram.com/boogaloozagreb/" },
                new Venue { Id = 6, Name = "Tvornica Kulture", Address = "Šubićeva 2", Latitude = 45.8085, Longitude = 15.9921, Capacity = 1000, WorkingHours = "20:00 - 04:00", ContactPhone = "+38514606650", Description = "Koncertni i klupski prostor s bogatim programom uživo.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=800", LogoUrl = "/img/logos/tvornica-emblem.png", InstagramUrl = "https://www.instagram.com/tvornicakulture/" },
                new Venue { Id = 7, Name = "Klub Močvara", Address = "Trnjanski nasip bb", Latitude = 45.79108, Longitude = 15.97639, Capacity = 600, WorkingHours = "20:00 - 03:00", ContactPhone = "+38516154290", Description = "Alternativni klub uz Savu, dom rock i underground scene.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=800", LogoUrl = "/img/logos/mocvara-emblem.png" },
                new Venue { Id = 8, Name = "Vintage Industrial Bar", Address = "Savska cesta 160", Latitude = 45.79023, Longitude = 15.95569, Capacity = 400, WorkingHours = "19:00 - 02:00", ContactPhone = "+38598123456", Description = "Industrijski bar s koncertima indie i rock bendova.", Type = VenueType.Bar, ImageUrl = "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800", LogoUrl = "/img/logos/vintage-emblem.png" },
                new Venue { Id = 9, Name = "Katran", Address = "Radnička cesta 27", Latitude = 45.8039, Longitude = 15.9998, Capacity = 800, WorkingHours = "23:00 - 07:00", ContactPhone = "+38591222333", Description = "Underground techno prostor u bivšoj tvornici.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=800", LogoUrl = "/img/logos/katran-emblem.jpg" },
                new Venue { Id = 10, Name = "Sirup Club", Address = "Radnička cesta 21", Latitude = 45.8040, Longitude = 16.0120, Capacity = 700, WorkingHours = "23:00 - 06:00", ContactPhone = "+38591444555", Description = "House i techno klub s vrhunskim sound systemom.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1545128485-c400e7702796?w=800", LogoUrl = "/img/logos/sirup-emblem.png", DeletedAt = new DateTime(2026, 6, 1) },
                new Venue { Id = 11, Name = "Opera Club", Address = "Savska cesta 141", Latitude = 45.79159, Longitude = 15.95720, Capacity = 900, WorkingHours = "23:00 - 05:00", ContactPhone = "+38591666777", Description = "Mainstream klub s komercijalnim hitovima i gostujućim DJ-evima.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=800", LogoUrl = "/img/logos/opera.svg" },
                new Venue { Id = 12, Name = "Masters Club", Address = "V. Ravnice 10", Latitude = 45.82273, Longitude = 16.03289, Capacity = 500, WorkingHours = "22:00 - 05:00", ContactPhone = "+38591888999", Description = "R'n'B i hip-hop klub u centru grada.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=800", LogoUrl = "/img/logos/masters.svg", InstagramUrl = "https://www.instagram.com/masters.zagreb/" },
                new Venue { Id = 13, Name = "Pločnik", Address = "Tkalčićeva 41", Latitude = 45.8150, Longitude = 15.9760, Capacity = 200, WorkingHours = "08:00 - 24:00", ContactPhone = "+38591101010", Description = "Popularni bar u Tkalči s DJ programom vikendom.", Type = VenueType.Bar, ImageUrl = "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=800", LogoUrl = "/img/logos/plocnik.svg", DeletedAt = new DateTime(2026, 6, 1) },
                new Venue { Id = 14, Name = "Hangar", Address = "Florijana Andrašeca 14", Latitude = 45.80163, Longitude = 15.96095, Capacity = 1000, WorkingHours = "23:00 - 08:00", ContactPhone = "+38591202020", Description = "Veliki techno prostor za rave partyje.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800", LogoUrl = "/img/logos/hangar-emblem.jpg", InstagramUrl = "https://www.instagram.com/hangarclubzgb/" },
                new Venue { Id = 15, Name = "Pogon Jedinstvo", Address = "Trnjanski nasip 23", Latitude = 45.7930, Longitude = 15.9780, Capacity = 600, WorkingHours = "20:00 - 04:00", ContactPhone = "+38591303030", Description = "Prostor za alternativnu i nezavisnu kulturu.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800", LogoUrl = "/img/logos/pogon-emblem.png", DeletedAt = new DateTime(2026, 6, 1) },
                new Venue { Id = 16, Name = "Lauba", Address = "Baruna Filipovića 23a", Latitude = 45.8120, Longitude = 15.9450, Capacity = 350, WorkingHours = "11:00 - 24:00", ContactPhone = "+38516323165", Description = "Prostor za umjetnost i događanja u bivšoj tvorničkoj hali.", Type = VenueType.Bar, ImageUrl = "https://images.unsplash.com/photo-1504333638930-c8787321eee0?w=800", LogoUrl = "/img/logos/lauba-emblem.png", DeletedAt = new DateTime(2026, 6, 1) },
                new Venue { Id = 17, Name = "Dom Sportova", Address = "Trg Krešimira Ćosića 11", Latitude = 45.8078, Longitude = 15.9518, Capacity = 6000, WorkingHours = "po programu", ContactPhone = "+38513650333", Description = "Dvorana za velike koncerte i događanja.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1540039155733-5bb30b53aa14?w=800", LogoUrl = "/img/logos/domsportova-emblem.jpg" },
                new Venue { Id = 18, Name = "Arena Zagreb", Address = "Vice Vukova 8", Latitude = 45.7715, Longitude = 15.9445, Capacity = 15000, WorkingHours = "po programu", ContactPhone = "+38516121111", Description = "Najveća koncertna arena u Zagrebu.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1429962714451-bb934ecdc4ec?w=800", LogoUrl = "/img/logos/arena-emblem.jpg" },
                new Venue { Id = 19, Name = "KD Vatroslav Lisinski", Address = "Trg Stjepana Radića 4", Latitude = 45.8004, Longitude = 15.9745, Capacity = 1850, WorkingHours = "po programu", ContactPhone = "+38516121166", Description = "Koncertna dvorana za klasičnu i ozbiljnu glazbu.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1465847899084-d164df4dedc6?w=800", LogoUrl = "/img/logos/lisinski-emblem.jpg" },
                new Venue { Id = 20, Name = "Jarun Plaža", Address = "Jezero Jarun", Latitude = 45.7820, Longitude = 15.9150, Capacity = 50000, WorkingHours = "open-air", ContactPhone = "+38591505050", Description = "Otvoreni prostor uz jezero, domaćin velikih festivala.", Type = VenueType.OpenAir, ImageUrl = "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=800", LogoUrl = "/img/logos/jarun.svg" },
                new Venue { Id = 21, Name = "Park Maksimir", Address = "Maksimirski perivoj", Latitude = 45.8260, Longitude = 16.0180, Capacity = 8000, WorkingHours = "open-air", ContactPhone = "+38591606060", Description = "Najveći zagrebački park, domaćin open-air događanja.", Type = VenueType.OpenAir, ImageUrl = "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=800", LogoUrl = "/img/logos/maksimir-emblem.png" },
                new Venue { Id = 22, Name = "Šalata", Address = "Schrottova ulica", Latitude = 45.8177, Longitude = 15.9838, Capacity = 2000, WorkingHours = "open-air", ContactPhone = "+38591707070", Description = "Ljetna pozornica i sportski centar na Šalati.", Type = VenueType.OpenAir, ImageUrl = "https://images.unsplash.com/photo-1492011221367-f47e3ccd77a0?w=800", LogoUrl = "/img/logos/salata.svg" },
                new Venue { Id = 23, Name = "Park Ribnjak", Address = "Ribnjak", Latitude = 45.8170, Longitude = 15.9810, Capacity = 1500, WorkingHours = "open-air", ContactPhone = "+38591808080", Description = "Gradski park s ljetnim chill događanjima.", Type = VenueType.OpenAir, ImageUrl = "https://images.unsplash.com/photo-1470770841072-f978cf4d019e?w=800", LogoUrl = "/img/logos/ribnjak.svg" },
                new Venue { Id = 24, Name = "Eli's Caffe", Address = "Ilica 63", Latitude = 45.8110, Longitude = 15.9650, Capacity = 60, WorkingHours = "07:00 - 20:00", ContactPhone = "+38591909090", Description = "Poznata kavana specijalizirana za vrhunsku kavu.", Type = VenueType.Cafe, ImageUrl = "https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800", LogoUrl = "/img/logos/eliscaffe.svg", DeletedAt = new DateTime(2026, 6, 1) },
                // ===== Dodatni poznati zagrebački klubovi =====
                new Venue { Id = 25, Name = "Club H2O", Address = "Runjaninova 3", Latitude = 45.80424, Longitude = 15.96973, Capacity = 600, WorkingHours = "23:00 - 06:00", ContactPhone = "+385915100661", Description = "Ekskluzivni noćni klub u centru Zagreba — priča koja drži vodu. 'Pure fun'.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=800", LogoUrl = "/img/logos/h2o-emblem.jpg", InstagramUrl = "https://www.instagram.com/clubh2ozagreb/", FloorPlanUrl = "/img/floorplans/h2o-tlocrt.svg" },
                new Venue { Id = 26, Name = "EX Club", Address = "Izidora Kršnjavoga 1", Latitude = 45.80627, Longitude = 15.96640, Capacity = 500, WorkingHours = "23:00 - 05:00", ContactPhone = "+385981112233", Description = "Živahan klub u Donjem gradu s balkan i pop programom.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800", LogoUrl = "/img/logos/ex-emblem.jpg" },
                new Venue { Id = 27, Name = "Osjećaj", Address = "Kačićeva 23", Latitude = 45.80843, Longitude = 15.96422, Capacity = 350, WorkingHours = "22:00 - 05:00", ContactPhone = "+385986700322", Description = "Klub i caffe bar u starom dijelu grada — slijedi osjećaj.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=800", LogoUrl = "/img/logos/osjecaj-emblem.svg", InstagramUrl = "https://www.instagram.com/slijedi_osjecaj/" },
                new Venue { Id = 28, Name = "Ritz Club", Address = "Florijana Andrašeca 14", Latitude = 45.80145, Longitude = 15.96091, Capacity = 700, WorkingHours = "23:00 - 06:00", ContactPhone = "+385985525500", Description = "Glamurozni noćni klub — najbolji doživljaj zagrebačkog noćnog života.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1545128485-c400e7702796?w=800", LogoUrl = "/img/logos/ritz-emblem.jpg", InstagramUrl = "https://www.instagram.com/ricklubzg/" },
                new Venue { Id = 29, Name = "THE Club", Address = "Bogovićeva 1a", Latitude = 45.81222, Longitude = 15.97483, Capacity = 400, WorkingHours = "23:00 - 05:00", ContactPhone = "+385991658675", Description = "Elegantni klub u samom centru — 'The' place to be.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=800", LogoUrl = "/img/logos/theclub-emblem.jpg", InstagramUrl = "https://www.instagram.com/theclubzagreb/" },
                new Venue { Id = 30, Name = "Mint Club & More", Address = "Florijana Andrašeca 14", Latitude = 45.80156, Longitude = 15.96129, Capacity = 800, WorkingHours = "23:00 - 06:00", ContactPhone = "+385913900707", Description = "Klub, vrt i više od toga — mint & more.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800", LogoUrl = "/img/logos/mint-emblem.jpg", InstagramUrl = "https://www.instagram.com/mintzagreb/" },
                new Venue { Id = 31, Name = "Club & Lounge Roko", Address = "Jarunska ulica", Latitude = 45.78387, Longitude = 15.94783, Capacity = 900, WorkingHours = "23:00 - 06:00", ContactPhone = "+385976592000", Description = "Klub i lounge stvoren za zabavu.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800", LogoUrl = "/img/logos/roko-emblem.jpg", InstagramUrl = "https://www.instagram.com/club_roko/" },
                new Venue { Id = 101, Name = "XO Club", Address = "Vlaška 9", Latitude = 45.81364, Longitude = 15.97979, Capacity = 300, WorkingHours = "23:00 - 05:00", ContactPhone = "+385911230032", Description = "Cocktail i party klub u Vlaškoj ulici.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=800", LogoUrl = "/img/logos/xo.svg" },
                new Venue { Id = 102, Name = "Noćni klub Sova", Address = "Adančeva ulica, Brckovljani (Dugo Selo)", Latitude = 45.82616, Longitude = 16.30361, Capacity = 400, WorkingHours = "22:00 - 06:00", ContactPhone = "+385911230033", Description = "Noćni klub istočno od Zagreba — party do zore.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800", LogoUrl = "/img/logos/sova-emblem.jpg", InstagramUrl = "https://www.instagram.com/sova.night.club/" },
                new Venue { Id = 103, Name = "Night Club Rocco", Address = "Andrije Hebranga 14", Latitude = 45.80943, Longitude = 15.97564, Capacity = 250, WorkingHours = "23:00 - 06:00", ContactPhone = "+385911230034", Description = "Noćni klub u srcu Donjeg grada.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1545128485-c400e7702796?w=800", LogoUrl = "/img/logos/rocco.svg" },
                new Venue { Id = 104, Name = "Alkatraz Rock Bar", Address = "Preradovićeva 12", Latitude = 45.81071, Longitude = 15.97428, Capacity = 200, WorkingHours = "21:00 - 04:00", ContactPhone = "+385911230035", Description = "Rock bar i noćni klub — glasne gitare do jutra.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800", LogoUrl = "/img/logos/alkatraz.svg" },
                new Venue { Id = 105, Name = "Bulldog Zagreb", Address = "Bogovićeva 6", Latitude = 45.81231, Longitude = 15.97518, Capacity = 300, WorkingHours = "09:00 - 02:00", ContactPhone = "+385911230036", Description = "Legendarni pub i bar na špici.", Type = VenueType.Bar, ImageUrl = "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=800", LogoUrl = "/img/logos/bulldog.svg" },
                new Venue { Id = 106, Name = "OUT Bunker Nightclub", Address = "Ilica 16", Latitude = 45.81345, Longitude = 15.97357, Capacity = 350, WorkingHours = "23:00 - 06:00", ContactPhone = "+385911230037", Description = "Underground klub u bunkeru ispod Ilice.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=800", LogoUrl = "/img/logos/outbunker.svg" },
                new Venue { Id = 107, Name = "OUT Rooftop", Address = "Ilica 16", Latitude = 45.81320, Longitude = 15.97350, Capacity = 200, WorkingHours = "20:00 - 02:00", ContactPhone = "+385911230038", Description = "Rooftop bar s pogledom na krovove Zagreba.", Type = VenueType.Bar, ImageUrl = "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=800", LogoUrl = "/img/logos/outrooftop-emblem.jpg", InstagramUrl = "https://www.instagram.com/outrooftop/" },
                new Venue { Id = 108, Name = "The Secret Club", Address = "Ulica kneza Borne 2", Latitude = 45.80733, Longitude = 15.98414, Capacity = 300, WorkingHours = "23:00 - 06:00", ContactPhone = "+385911230039", Description = "Skriveni klub za one koji znaju.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=800", LogoUrl = "/img/logos/secret.svg" }
            );

            // ====== EVENTI ZA NOVE LOKACIJE (nadolazeći) ======
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 10, Name = "Aquarius Summer Opening", Description = "Veliko otvorenje ljetne sezone na Jarunu uz top elektronske DJ-eve.", StartTime = new DateTime(2026, 6, 20, 23, 0, 0), EndTime = new DateTime(2026, 6, 21, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 20.00m, PosterUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", AgeLimit = 18, IsFeatured = true, VenueId = 4 },
                new Event { Id = 11, Name = "Hladno Pivo Live", Description = "Legendarni zagrebački bend uživo u Boogaloou.", StartTime = new DateTime(2026, 7, 4, 21, 0, 0), EndTime = new DateTime(2026, 7, 5, 1, 0, 0), Type = EventType.Concert, EntryPrice = 25.00m, PosterUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", AgeLimit = 16, VenueId = 5 },
                new Event { Id = 12, Name = "Tvornica Techno Night", Description = "Noć techno glazbe s rezidentnim i gostujućim DJ-evima.", StartTime = new DateTime(2026, 6, 19, 23, 0, 0), EndTime = new DateTime(2026, 6, 20, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 15.00m, PosterUrl = "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", AgeLimit = 18, VenueId = 6 },
                new Event { Id = 13, Name = "Punk Rock Večer", Description = "Underground punk i rock bendovi u Močvari.", StartTime = new DateTime(2026, 6, 21, 21, 0, 0), EndTime = new DateTime(2026, 6, 22, 2, 0, 0), Type = EventType.Concert, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=600", AgeLimit = 16, VenueId = 7 },
                new Event { Id = 14, Name = "Indie Live Session", Description = "Akustični i indie nastupi u Vintage Industrial baru.", StartTime = new DateTime(2026, 6, 18, 20, 0, 0), EndTime = new DateTime(2026, 6, 18, 23, 30, 0), Type = EventType.Concert, EntryPrice = 8.00m, PosterUrl = "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", AgeLimit = 0, VenueId = 8 },
                new Event { Id = 15, Name = "Katran Underground: Techno", Description = "Cijela noć techna u industrijskom ambijentu Katrana.", StartTime = new DateTime(2026, 6, 27, 23, 30, 0), EndTime = new DateTime(2026, 6, 28, 7, 0, 0), Type = EventType.DJNight, EntryPrice = 18.00m, PosterUrl = "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", AgeLimit = 18, VenueId = 9 },
                new Event { Id = 16, Name = "House Nation", Description = "Najbolji house DJ-evi grada na jednom mjestu.", StartTime = new DateTime(2026, 7, 5, 23, 0, 0), EndTime = new DateTime(2026, 7, 6, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 16.00m, PosterUrl = "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", AgeLimit = 18, VenueId = 10, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 17, Name = "Opera Saturday", Description = "Subotnja party noć s komercijalnim hitovima.", StartTime = new DateTime(2026, 6, 28, 23, 0, 0), EndTime = new DateTime(2026, 6, 29, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 12.00m, PosterUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", AgeLimit = 18, VenueId = 11 },
                new Event { Id = 18, Name = "Masters R'n'B Night", Description = "R'n'B i hip-hop klasici cijelu noć.", StartTime = new DateTime(2026, 7, 11, 22, 0, 0), EndTime = new DateTime(2026, 7, 12, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 14.00m, PosterUrl = "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", AgeLimit = 18, VenueId = 12 },
                new Event { Id = 19, Name = "Pločnik Acoustic", Description = "Opušteni akustični nastup u srcu Tkalče.", StartTime = new DateTime(2026, 6, 25, 20, 0, 0), EndTime = new DateTime(2026, 6, 25, 23, 0, 0), Type = EventType.Concert, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", AgeLimit = 0, VenueId = 13, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 20, Name = "Hangar Rave", Description = "Industrijski rave s europskim techno headlinerima.", StartTime = new DateTime(2026, 7, 12, 23, 30, 0), EndTime = new DateTime(2026, 7, 13, 9, 0, 0), Type = EventType.DJNight, EntryPrice = 22.00m, PosterUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", AgeLimit = 18, VenueId = 14 },
                new Event { Id = 21, Name = "Alternativna Scena", Description = "Nezavisni bendovi i izvođači u Pogonu.", StartTime = new DateTime(2026, 6, 26, 20, 0, 0), EndTime = new DateTime(2026, 6, 27, 1, 0, 0), Type = EventType.Concert, EntryPrice = 7.00m, PosterUrl = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", AgeLimit = 0, VenueId = 15, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 22, Name = "Art & Beats", Description = "Spoj umjetničke izložbe i DJ seta u Laubi.", StartTime = new DateTime(2026, 7, 3, 19, 0, 0), EndTime = new DateTime(2026, 7, 4, 1, 0, 0), Type = EventType.DJNight, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1504333638930-c8787321eee0?w=600", AgeLimit = 18, VenueId = 16, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 23, Name = "Gibonni Live", Description = "Veliki koncert Gibonnija u Domu sportova.", StartTime = new DateTime(2026, 9, 15, 20, 0, 0), EndTime = new DateTime(2026, 9, 15, 23, 0, 0), Type = EventType.Concert, EntryPrice = 35.00m, PosterUrl = "https://images.unsplash.com/photo-1540039155733-5bb30b53aa14?w=600", AgeLimit = 0, VenueId = 17 },
                new Event { Id = 24, Name = "Severina Spektakl", Description = "Veliki pop spektakl u Areni Zagreb.", StartTime = new DateTime(2026, 9, 20, 20, 0, 0), EndTime = new DateTime(2026, 9, 20, 23, 30, 0), Type = EventType.Concert, EntryPrice = 40.00m, PosterUrl = "https://images.unsplash.com/photo-1429962714451-bb934ecdc4ec?w=600", AgeLimit = 0, IsFeatured = true, VenueId = 18 },
                new Event { Id = 25, Name = "Zagrebačka Filharmonija", Description = "Večer klasične glazbe u dvorani Lisinski.", StartTime = new DateTime(2026, 6, 30, 20, 0, 0), EndTime = new DateTime(2026, 6, 30, 22, 0, 0), Type = EventType.Concert, EntryPrice = 28.00m, PosterUrl = "https://images.unsplash.com/photo-1465847899084-d164df4dedc6?w=600", AgeLimit = 0, VenueId = 19 },
                new Event { Id = 26, Name = "INmusic Festival", Description = "Najveći hrvatski open-air glazbeni festival na Jarunu.", StartTime = new DateTime(2026, 6, 22, 16, 0, 0), EndTime = new DateTime(2026, 6, 25, 2, 0, 0), Type = EventType.Festival, EntryPrice = 89.00m, PosterUrl = "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", AgeLimit = 16, IsFeatured = true, VenueId = 20 },
                new Event { Id = 27, Name = "Maksimir Open Air", Description = "Cjelodnevni open-air festival u parku Maksimir.", StartTime = new DateTime(2026, 7, 18, 14, 0, 0), EndTime = new DateTime(2026, 7, 19, 1, 0, 0), Type = EventType.Festival, EntryPrice = 30.00m, PosterUrl = "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", AgeLimit = 16, VenueId = 21 },
                new Event { Id = 28, Name = "Šalata Summer Sessions", Description = "Ljetne DJ večeri pod zvijezdama na Šalati.", StartTime = new DateTime(2026, 7, 25, 21, 0, 0), EndTime = new DateTime(2026, 7, 26, 3, 0, 0), Type = EventType.DJNight, EntryPrice = 12.00m, PosterUrl = "https://images.unsplash.com/photo-1492011221367-f47e3ccd77a0?w=600", AgeLimit = 18, VenueId = 22 },
                new Event { Id = 29, Name = "Ribnjak Chill Sessions", Description = "Opuštene DJ večeri u parku Ribnjak.", StartTime = new DateTime(2026, 6, 17, 19, 0, 0), EndTime = new DateTime(2026, 6, 17, 23, 0, 0), Type = EventType.DJNight, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1470770841072-f978cf4d019e?w=600", AgeLimit = 0, VenueId = 23 },
                new Event { Id = 30, Name = "Coffee & Jazz", Description = "Jutarnji jazz uz vrhunsku kavu u Eli's Caffeu.", StartTime = new DateTime(2026, 6, 16, 10, 0, 0), EndTime = new DateTime(2026, 6, 16, 13, 0, 0), Type = EventType.Concert, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", AgeLimit = 0, VenueId = 24, DeletedAt = new DateTime(2026, 6, 1) },
                // Novi nadolazeći eventi za postojeće lokacije (da se prikažu na karti)
                new Event { Id = 31, Name = "Summer Techno Marathon", Description = "Cjelonoćni techno maraton u Club Cultureu.", StartTime = new DateTime(2026, 7, 19, 23, 0, 0), EndTime = new DateTime(2026, 7, 20, 7, 0, 0), Type = EventType.DJNight, EntryPrice = 18.00m, PosterUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", AgeLimit = 18, VenueId = 1, DeletedAt = new DateTime(2026, 6, 1) },
                new Event { Id = 32, Name = "Ljetni Pub Quiz", Description = "Tjedni pub kviz u Kavani Lav, ljetno izdanje.", StartTime = new DateTime(2026, 6, 24, 19, 0, 0), EndTime = new DateTime(2026, 6, 24, 22, 0, 0), Type = EventType.PubQuiz, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1606761568499-6d2451b23c66?w=600", AgeLimit = 0, VenueId = 2, DeletedAt = new DateTime(2026, 6, 1) },
                // ===== Eventi za dodatne klubove =====
                new Event { Id = 33, Name = "H2O Pure Fun Opening", Description = "Veliko otvorenje sezone u Clubu H2O uz top DJ-eve.", StartTime = new DateTime(2026, 7, 10, 23, 0, 0), EndTime = new DateTime(2026, 7, 11, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 20.00m, PosterUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", AgeLimit = 18, IsFeatured = true, VenueId = 25 },
                new Event { Id = 34, Name = "EX Balkan Night", Description = "Najbolji balkan i pop hitovi cijelu noć.", StartTime = new DateTime(2026, 7, 17, 23, 0, 0), EndTime = new DateTime(2026, 7, 18, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=600", AgeLimit = 18, VenueId = 26 },
                new Event { Id = 35, Name = "Osjećaj Live Session", Description = "Live nastup uz koktele u klubu Osjećaj.", StartTime = new DateTime(2026, 7, 9, 22, 0, 0), EndTime = new DateTime(2026, 7, 10, 3, 0, 0), Type = EventType.Concert, EntryPrice = 8.00m, PosterUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", AgeLimit = 18, VenueId = 27 },
                new Event { Id = 36, Name = "Ritz Glamour Night", Description = "Glamurozna subotnja noć uz rezidentne DJ-eve.", StartTime = new DateTime(2026, 7, 18, 23, 0, 0), EndTime = new DateTime(2026, 7, 19, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 15.00m, PosterUrl = "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", AgeLimit = 18, VenueId = 28 },
                new Event { Id = 37, Name = "THE Opening Party", Description = "Otvaranje sezone u najekskluzivnijem klubu centra.", StartTime = new DateTime(2026, 7, 11, 23, 0, 0), EndTime = new DateTime(2026, 7, 12, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 18.00m, PosterUrl = "https://images.unsplash.com/photo-1438557068880-c5f474830377?w=600", AgeLimit = 18, VenueId = 29 },
                new Event { Id = 38, Name = "Mint Garden Sessions", Description = "House i techno u vrtu kluba Mint.", StartTime = new DateTime(2026, 7, 24, 23, 0, 0), EndTime = new DateTime(2026, 7, 25, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 16.00m, PosterUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", AgeLimit = 18, VenueId = 30 },
                new Event { Id = 39, Name = "Roko Fešta", Description = "Domaća zabava i hitovi u Club & Lounge Roko.", StartTime = new DateTime(2026, 7, 12, 23, 0, 0), EndTime = new DateTime(2026, 7, 13, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 12.00m, PosterUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", AgeLimit = 18, VenueId = 31 },
                new Event { Id = 111, Name = "XO Friday Party", Description = "Petak navečer u XO klubu uz house i pop hitove.", StartTime = new DateTime(2026, 7, 17, 23, 0, 0), EndTime = new DateTime(2026, 7, 18, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1516450360452-9312f5e86fc7?w=600", AgeLimit = 18, VenueId = 101 },
                new Event { Id = 112, Name = "Sova Night Fever", Description = "Vikend party u Sovi — do zore.", StartTime = new DateTime(2026, 7, 18, 22, 0, 0), EndTime = new DateTime(2026, 7, 19, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 8.00m, PosterUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", AgeLimit = 18, VenueId = 102 },
                new Event { Id = 113, Name = "Rocco Weekend Night", Description = "Vikend zabava u Roccu.", StartTime = new DateTime(2026, 7, 17, 23, 0, 0), EndTime = new DateTime(2026, 7, 18, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1545128485-c400e7702796?w=600", AgeLimit = 18, VenueId = 103 },
                new Event { Id = 114, Name = "Alkatraz Rock Night", Description = "Živa rock svirka i glasne gitare.", StartTime = new DateTime(2026, 7, 16, 21, 0, 0), EndTime = new DateTime(2026, 7, 17, 2, 0, 0), Type = EventType.Concert, EntryPrice = 8.00m, PosterUrl = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=600", AgeLimit = 0, VenueId = 104 },
                new Event { Id = 115, Name = "Bulldog Pub Quiz", Description = "Tjedni kviz znanja na špici — ekipe do 6 igrača.", StartTime = new DateTime(2026, 7, 15, 20, 0, 0), EndTime = new DateTime(2026, 7, 15, 22, 30, 0), Type = EventType.PubQuiz, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1514933651103-005eec06c04b?w=600", AgeLimit = 0, VenueId = 105 },
                new Event { Id = 116, Name = "Bunker Techno Session", Description = "Underground techno u bunkeru ispod Ilice.", StartTime = new DateTime(2026, 7, 18, 23, 30, 0), EndTime = new DateTime(2026, 7, 19, 6, 0, 0), Type = EventType.DJNight, EntryPrice = 12.00m, PosterUrl = "https://images.unsplash.com/photo-1574391884720-bbc3740c59d1?w=600", AgeLimit = 18, VenueId = 106 },
                new Event { Id = 117, Name = "Rooftop Sunset DJ", Description = "Zalazak sunca uz DJ-a na krovu Ilice 16.", StartTime = new DateTime(2026, 7, 16, 20, 0, 0), EndTime = new DateTime(2026, 7, 17, 1, 0, 0), Type = EventType.DJNight, EntryPrice = 5.00m, PosterUrl = "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", AgeLimit = 0, VenueId = 107 },
                new Event { Id = 118, Name = "Secret Saturday", Description = "Subota koja se ne priča dalje.", StartTime = new DateTime(2026, 7, 18, 23, 0, 0), EndTime = new DateTime(2026, 7, 19, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 12.00m, PosterUrl = "https://images.unsplash.com/photo-1492684223066-81342ee5ff30?w=600", AgeLimit = 18, VenueId = 108 }
            );

            // ====== STOLOVI ZA NOVE LOKACIJE (2 po lokaciji) ======
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 12, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 4 },
                new Table { Id = 13, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 4 },
                new Table { Id = 14, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 5 },
                new Table { Id = 15, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 5 },
                new Table { Id = 16, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 6 },
                new Table { Id = 17, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 6 },
                new Table { Id = 18, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, VenueId = 7 },
                new Table { Id = 19, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 7 },
                new Table { Id = 20, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, VenueId = 8 },
                new Table { Id = 21, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, VenueId = 8 },
                new Table { Id = 22, TableNumber = 1, SeatCount = 8, Zone = TableZone.VIP, VenueId = 9 },
                new Table { Id = 23, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 9 },
                new Table { Id = 24, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 10 },
                new Table { Id = 25, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 10 },
                new Table { Id = 26, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 11 },
                new Table { Id = 27, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 11 },
                new Table { Id = 28, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 12 },
                new Table { Id = 29, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 12 },
                new Table { Id = 30, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, VenueId = 13 },
                new Table { Id = 31, TableNumber = 2, SeatCount = 2, Zone = TableZone.Regular, VenueId = 13 },
                new Table { Id = 32, TableNumber = 1, SeatCount = 8, Zone = TableZone.VIP, VenueId = 14 },
                new Table { Id = 33, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 14 },
                new Table { Id = 34, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, VenueId = 15 },
                new Table { Id = 35, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 15 },
                new Table { Id = 36, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, VenueId = 16 },
                new Table { Id = 37, TableNumber = 2, SeatCount = 4, Zone = TableZone.Regular, VenueId = 16 },
                new Table { Id = 38, TableNumber = 1, SeatCount = 4, Zone = TableZone.VIP, VenueId = 20 },
                new Table { Id = 39, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, VenueId = 20 },
                new Table { Id = 40, TableNumber = 1, SeatCount = 4, Zone = TableZone.VIP, VenueId = 22 },
                new Table { Id = 41, TableNumber = 2, SeatCount = 4, Zone = TableZone.VIP, VenueId = 22 }
            );

            // ====== GENERIRANI STOLOVI ======
            // Klubovi: popuna do 30 stolova (1-8 VIP po 8 mjesta, 9-18 Regular po 6, 19-30 Regular po 4).
            // Festivali/open-air: SAMO VIP stolovi (popuna do 10, po 8 mjesta).
            // Bar (Vintage): popuna do 12 stolova.
            var genTables = new List<Table>();
            int tid = 100; // generirani idu od 100 da ne diraju rucni seed (1-41)

            void FillClub(int venueId, int startNumber)
            {
                for (int n = startNumber; n <= 30; n++)
                    genTables.Add(new Table
                    {
                        Id = tid++,
                        TableNumber = n,
                        SeatCount = n <= 8 ? 8 : (n <= 18 ? 6 : 4),
                        Zone = n <= 8 ? TableZone.VIP : TableZone.Regular,
                        VenueId = venueId
                    });
            }

            void FillOpenAirVip(int venueId, int startNumber)
            {
                for (int n = startNumber; n <= 10; n++)
                    genTables.Add(new Table { Id = tid++, TableNumber = n, SeatCount = 8, Zone = TableZone.VIP, VenueId = venueId });
            }

            // klubovi/dvorane koji vec imaju 2 stola -> od broja 3
            foreach (var vid in new[] { 4, 5, 6, 7, 9, 12, 14 }) FillClub(vid, 3);
            // klubovi/dvorane bez stolova -> od broja 1
            foreach (var vid in new[] { 17, 18, 19, 25, 26, 27, 28, 29, 30, 31 }) FillClub(vid, 1);
            // bar Vintage (id 8, ima 2) -> do 12 stolova
            for (int n = 3; n <= 12; n++)
                genTables.Add(new Table { Id = tid++, TableNumber = n, SeatCount = 4, Zone = n <= 4 ? TableZone.VIP : TableZone.Regular, VenueId = 8 });
            // festivali / open-air: samo VIP
            FillOpenAirVip(3, 5);   // Bundek vec ima 4
            FillOpenAirVip(20, 3);  // Jarun ima 2
            FillOpenAirVip(21, 1);  // Maksimir
            FillOpenAirVip(22, 3);  // Salata ima 2
            FillOpenAirVip(23, 1);  // Ribnjak

            // Pozicije stolova H2O (venue 25) na tlocrtu h2o-tlocrt.svg (800x600 -> postoci)
            var h2oPos = new (double x, double y)[]
            {
                (70,100),(150,100),(70,180),(150,180),      // 1-4 VIP lijevo
                (650,100),(730,100),(650,180),(730,180),    // 5-8 VIP desno
                (70,270),(150,270),(70,350),(150,350),(110,430),   // 9-13 lijevi bok
                (650,270),(730,270),(650,350),(730,350),(690,430), // 14-18 desni bok
                (250,370),(310,370),(370,370),(430,370),(490,370),(550,370), // 19-24
                (250,440),(310,440),(370,440),(430,440),(490,440),(550,440)  // 25-30
            };
            foreach (var t in genTables.Where(t => t.VenueId == 25))
            {
                var p = h2oPos[t.TableNumber - 1];
                t.PosX = Math.Round(p.x / 8.0, 2);   // 800px sirine -> %
                t.PosY = Math.Round(p.y / 6.0, 2);   // 600px visine -> %
            }

            modelBuilder.Entity<Table>().HasData(genTables);
        }
    }
}
