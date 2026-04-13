using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Repositories
{
    public static class MockData
    {
        public static List<User> Users { get; }
        public static List<Venue> Venues { get; }
        public static List<Event> Events { get; }
        public static List<Reservation> Reservations { get; }
        public static List<Review> Reviews { get; }

        static MockData()
        {
            // KORISNICI
            var ivan = new User { Id = 1, FirstName = "Ivan", LastName = "Golubić", DateOfBirth = new DateTime(2003, 5, 15), Email = "ivan.golubic@email.com", PhoneNumber = "+385911234567", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 1, 10) };
            var ana = new User { Id = 2, FirstName = "Ana", LastName = "Horvat", DateOfBirth = new DateTime(2001, 8, 22), Email = "ana.horvat@email.com", PhoneNumber = "+385917654321", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 2, 5) };
            var marko = new User { Id = 3, FirstName = "Marko", LastName = "Kovačević", DateOfBirth = new DateTime(1990, 3, 10), Email = "marko.kovacevic@email.com", PhoneNumber = "+385921112233", Role = UserRole.Owner, RegisteredAt = new DateTime(2025, 11, 1) };
            var petra = new User { Id = 4, FirstName = "Petra", LastName = "Babić", DateOfBirth = new DateTime(2000, 12, 1), Email = "petra.babic@email.com", PhoneNumber = "+385998887766", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 3, 1) };
            var admin = new User { Id = 5, FirstName = "Luka", LastName = "Perić", DateOfBirth = new DateTime(1985, 7, 20), Email = "luka.peric@admin.com", PhoneNumber = "+385915556677", Role = UserRole.Admin, RegisteredAt = new DateTime(2025, 6, 1) };
            var karlo = new User { Id = 6, FirstName = "Karlo", LastName = "Novak", DateOfBirth = new DateTime(2010, 9, 25), Email = "karlo.novak@email.com", PhoneNumber = "+385912223344", Role = UserRole.Guest, RegisteredAt = new DateTime(2026, 3, 20) };

            // VENUES
            var clubCulture = new Venue { Id = 1, Name = "Club Culture", Address = "Jabukovac 10, Zagreb", Latitude = 45.8144, Longitude = 15.9786, Capacity = 500, WorkingHours = "22:00 - 05:00", ContactPhone = "+38514567890", Description = "Najpoznatiji noćni klub u Zagrebu s vrhunskim DJ programom.", Type = VenueType.Club, ImageUrl = "https://images.unsplash.com/photo-1566737236500-c8ac43014a67?w=800" };
            var kavanaLav = new Venue { Id = 2, Name = "Kavana Lav", Address = "Ilica 45, Zagreb", Latitude = 45.8131, Longitude = 15.9665, Capacity = 80, WorkingHours = "08:00 - 23:00", ContactPhone = "+38511234567", Description = "Ugodan kafić u centru Zagreba s pub kviz večerima.", Type = VenueType.Cafe, ImageUrl = "https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800" };
            var parkStage = new Venue { Id = 3, Name = "Park Stage Bundek", Address = "Bundek, Novi Zagreb", Latitude = 45.7869, Longitude = 15.9874, Capacity = 5000, WorkingHours = "16:00 - 02:00", ContactPhone = "+38519876543", Description = "Open-air pozornica pored jezera Bundek za festivale i koncerte.", Type = VenueType.OpenAir, ImageUrl = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?w=800" };

            // STOLOVI
            var ct1 = new Table { Id = 1, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, Venue = clubCulture };
            var ct2 = new Table { Id = 2, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, Venue = clubCulture };
            var ct3 = new Table { Id = 3, TableNumber = 3, SeatCount = 8, Zone = TableZone.VIP, Venue = clubCulture };
            var ct4 = new Table { Id = 4, TableNumber = 4, SeatCount = 4, Zone = TableZone.Regular, Venue = clubCulture };
            clubCulture.Tables.AddRange(new[] { ct1, ct2, ct3, ct4 });

            var lt1 = new Table { Id = 5, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, Venue = kavanaLav };
            var lt2 = new Table { Id = 6, TableNumber = 2, SeatCount = 6, Zone = TableZone.Regular, Venue = kavanaLav };
            var lt3 = new Table { Id = 7, TableNumber = 3, SeatCount = 4, Zone = TableZone.Regular, Venue = kavanaLav };
            kavanaLav.Tables.AddRange(new[] { lt1, lt2, lt3 });

            var pt1 = new Table { Id = 8, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, Venue = parkStage };
            var pt2 = new Table { Id = 9, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, Venue = parkStage };
            var pt3 = new Table { Id = 10, TableNumber = 3, SeatCount = 4, Zone = TableZone.Regular, Venue = parkStage };
            var pt4 = new Table { Id = 11, TableNumber = 4, SeatCount = 4, Zone = TableZone.Regular, Venue = parkStage };
            parkStage.Tables.AddRange(new[] { pt1, pt2, pt3, pt4 });

            // CJENIK
            clubCulture.PriceList.AddRange(new[] {
                new PriceListItem { Id = 1, ItemName = "Gin & Tonic", Price = 8.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 2, ItemName = "Vodka Red Bull", Price = 9.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 3, ItemName = "Jack & Coke", Price = 10.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 4, ItemName = "Heineken 0.5l", Price = 5.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 5, ItemName = "VIP ulaz", Price = 30.00m, Category = "Ulaznica", Venue = clubCulture }
            });
            kavanaLav.PriceList.AddRange(new[] {
                new PriceListItem { Id = 6, ItemName = "Espresso", Price = 1.50m, Category = "Piće", Venue = kavanaLav },
                new PriceListItem { Id = 7, ItemName = "Cappuccino", Price = 2.50m, Category = "Piće", Venue = kavanaLav },
                new PriceListItem { Id = 8, ItemName = "Craft pivo", Price = 5.00m, Category = "Piće", Venue = kavanaLav },
                new PriceListItem { Id = 9, ItemName = "Sendvič", Price = 4.50m, Category = "Hrana", Venue = kavanaLav }
            });
            parkStage.PriceList.AddRange(new[] {
                new PriceListItem { Id = 10, ItemName = "Pivo 0.5l", Price = 4.00m, Category = "Piće", Venue = parkStage },
                new PriceListItem { Id = 11, ItemName = "Kokteli", Price = 7.00m, Category = "Piće", Venue = parkStage },
                new PriceListItem { Id = 12, ItemName = "Pizza komad", Price = 3.50m, Category = "Hrana", Venue = parkStage },
                new PriceListItem { Id = 13, ItemName = "Festival pass", Price = 50.00m, Category = "Ulaznica", Venue = parkStage }
            });

            // EVENTI
            var e1 = new Event { Id = 1, Name = "Techno Night ft. MLADY", Description = "Najbolja techno večer u gradu s rezidentnim DJ-em MLADY koji dolazi direktno iz Berlina.", StartTime = new DateTime(2026, 4, 25, 23, 0, 0), EndTime = new DateTime(2026, 4, 26, 5, 0, 0), Type = EventType.DJNight, EntryPrice = 15.00m, PosterUrl = "https://images.unsplash.com/photo-1571266028243-e4733b0f0bb0?w=600", AgeLimit = 18, Venue = clubCulture };
            var e2 = new Event { Id = 2, Name = "Vojko V Live", Description = "Vojko V dolazi u Club Culture na ekskluzivni nastup! Jedna od najpopularnijih domaćih glazbenih zvezda.", StartTime = new DateTime(2026, 5, 12, 22, 0, 0), EndTime = new DateTime(2026, 5, 13, 3, 0, 0), Type = EventType.Concert, EntryPrice = 25.00m, PosterUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=600", AgeLimit = 18, Venue = clubCulture };
            var e3 = new Event { Id = 3, Name = "Retro Party 90s", Description = "Vratite se u 90-te uz najbolje hitove!", StartTime = new DateTime(2026, 3, 15, 22, 0, 0), EndTime = new DateTime(2026, 3, 16, 4, 0, 0), Type = EventType.DJNight, EntryPrice = 10.00m, PosterUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=600", AgeLimit = 18, Venue = clubCulture };
            var e4 = new Event { Id = 4, Name = "Pub Quiz Srijeda", Description = "Testiraj svoje znanje svaku srijedu! Nagrada za 1. mjesto.", StartTime = new DateTime(2026, 4, 29, 19, 0, 0), EndTime = new DateTime(2026, 4, 29, 22, 0, 0), Type = EventType.PubQuiz, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1606761568499-6d2451b23c66?w=600", AgeLimit = 0, Venue = kavanaLav };
            var e5 = new Event { Id = 5, Name = "Acoustic Night - Lokalni bendovi", Description = "Akustični nastupi lokalnih bendova uz craft pivo.", StartTime = new DateTime(2026, 5, 9, 20, 0, 0), EndTime = new DateTime(2026, 5, 9, 23, 0, 0), Type = EventType.Concert, EntryPrice = 5.00m, PosterUrl = "https://images.unsplash.com/photo-1511735111819-9a3efd16269a?w=600", AgeLimit = 0, Venue = kavanaLav };
            var e6 = new Event { Id = 6, Name = "Pub Quiz - Filmska Tematika", Description = "Specijalni pub quiz o filmovima.", StartTime = new DateTime(2026, 3, 19, 19, 0, 0), EndTime = new DateTime(2026, 3, 19, 22, 0, 0), Type = EventType.PubQuiz, EntryPrice = 0.00m, PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=600", AgeLimit = 0, Venue = kavanaLav };
            var e7 = new Event { Id = 7, Name = "Zagreb Summer Beats", Description = "Dvodnevni festival elektronske glazbe na Bundeku s top europskim DJ-evima.", StartTime = new DateTime(2026, 6, 20, 16, 0, 0), EndTime = new DateTime(2026, 6, 22, 2, 0, 0), Type = EventType.Festival, EntryPrice = 50.00m, PosterUrl = "https://images.unsplash.com/photo-1506157786151-b8491531f063?w=600", AgeLimit = 18, Venue = parkStage };
            var e8 = new Event { Id = 8, Name = "Let 3 - Bundek Open Air", Description = "Legendarni Let 3 na pozornici Bundek!", StartTime = new DateTime(2026, 5, 10, 20, 0, 0), EndTime = new DateTime(2026, 5, 10, 23, 30, 0), Type = EventType.Concert, EntryPrice = 20.00m, PosterUrl = "https://images.unsplash.com/photo-1501386761578-eac5c94b800a?w=600", AgeLimit = 0, Venue = parkStage };
            var e9 = new Event { Id = 9, Name = "Spring Vibes Festival", Description = "Proljetni mini-festival s lokalnim DJ-evima.", StartTime = new DateTime(2026, 3, 22, 17, 0, 0), EndTime = new DateTime(2026, 3, 22, 23, 0, 0), Type = EventType.Festival, EntryPrice = 15.00m, PosterUrl = "https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?w=600", AgeLimit = 16, Venue = parkStage };

            clubCulture.Events.AddRange(new[] { e1, e2, e3 });
            kavanaLav.Events.AddRange(new[] { e4, e5, e6 });
            parkStage.Events.AddRange(new[] { e7, e8, e9 });

            // REZERVACIJE
            var r1 = new Reservation { Id = 1, CreatedAt = new DateTime(2026, 3, 28), NumberOfGuests = 4, Status = ReservationStatus.Confirmed, Note = "Rođendan, molimo balon dekoraciju", MinimumSpending = 100.00m, User = ivan, Table = ct2, Event = e1 };
            var r2 = new Reservation { Id = 2, CreatedAt = new DateTime(2026, 3, 29), NumberOfGuests = 6, Status = ReservationStatus.Pending, Note = "", MinimumSpending = 150.00m, User = ana, Table = ct3, Event = e2 };
            var r3 = new Reservation { Id = 3, CreatedAt = new DateTime(2026, 3, 30), NumberOfGuests = 3, Status = ReservationStatus.Confirmed, Note = "Blizu pozornice ako je moguće", MinimumSpending = 0.00m, User = petra, Table = lt1, Event = e4 };
            var r4 = new Reservation { Id = 4, CreatedAt = new DateTime(2026, 3, 25), NumberOfGuests = 5, Status = ReservationStatus.Cancelled, Note = "Otkazano zbog bolesti", MinimumSpending = 200.00m, User = ivan, Table = pt1, Event = e7 };
            var r5 = new Reservation { Id = 5, CreatedAt = new DateTime(2026, 3, 31), NumberOfGuests = 4, Status = ReservationStatus.Confirmed, Note = "", MinimumSpending = 0.00m, User = ana, Table = pt3, Event = e8 };

            e1.Reservations.Add(r1);
            e2.Reservations.Add(r2);
            e4.Reservations.Add(r3);
            e7.Reservations.Add(r4);
            e8.Reservations.Add(r5);

            ivan.Reservations.AddRange(new[] { r1, r4 });
            ana.Reservations.AddRange(new[] { r2, r5 });
            petra.Reservations.Add(r3);

            // RECENZIJE
            var rv1 = new Review { Id = 1, Rating = 5, Comment = "Odlična atmosfera, DJ je bio fenomenalan!", CreatedAt = new DateTime(2026, 3, 16), User = ivan, Event = e3 };
            var rv2 = new Review { Id = 2, Rating = 4, Comment = "Super quiz, pitanja su bila zanimljiva.", CreatedAt = new DateTime(2026, 3, 20), User = ana, Event = e6 };
            var rv3 = new Review { Id = 3, Rating = 3, Comment = "Bilo je OK, ali predugo čekanje za piće.", CreatedAt = new DateTime(2026, 3, 16), User = petra, Event = e3 };
            var rv4 = new Review { Id = 4, Rating = 5, Comment = "Najbolji festival ove godine, 10/10!", CreatedAt = new DateTime(2026, 3, 23), User = ivan, Event = e9 };
            var rv5 = new Review { Id = 5, Rating = 4, Comment = "Dobar vibe, lokacija predivna.", CreatedAt = new DateTime(2026, 3, 23), User = marko, Event = e9 };

            e3.Reviews.AddRange(new[] { rv1, rv3 });
            e6.Reviews.Add(rv2);
            e9.Reviews.AddRange(new[] { rv4, rv5 });

            ivan.Reviews.AddRange(new[] { rv1, rv4 });
            ana.Reviews.Add(rv2);
            petra.Reviews.Add(rv3);
            marko.Reviews.Add(rv5);

            ivan.FavoriteVenues.AddRange(new[] { clubCulture, parkStage });
            ana.FavoriteVenues.Add(kavanaLav);
            petra.FavoriteVenues.AddRange(new[] { kavanaLav, clubCulture });

            Users = new List<User> { ivan, ana, marko, petra, admin, karlo };
            Venues = new List<Venue> { clubCulture, kavanaLav, parkStage };
            Events = new List<Event> { e1, e2, e3, e4, e5, e6, e7, e8, e9 };
            Reservations = new List<Reservation> { r1, r2, r3, r4, r5 };
            Reviews = new List<Review> { rv1, rv2, rv3, rv4, rv5 };
        }
    }
}
