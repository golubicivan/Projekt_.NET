using System;
using ZagrebEvents.Model;

namespace ZagrebEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            // =============================================
            // KREIRANJE KORISNIKA
            // =============================================
            var ivan = new User
            {
                Id = 1,
                FirstName = "Ivan",
                LastName = "Golubić",
                DateOfBirth = new DateTime(2003, 5, 15),
                Email = "ivan.golubic@email.com",
                PhoneNumber = "+385911234567",
                Role = UserRole.Guest,
                RegisteredAt = new DateTime(2026, 1, 10)
            };

            var ana = new User
            {
                Id = 2,
                FirstName = "Ana",
                LastName = "Horvat",
                DateOfBirth = new DateTime(2001, 8, 22),
                Email = "ana.horvat@email.com",
                PhoneNumber = "+385917654321",
                Role = UserRole.Guest,
                RegisteredAt = new DateTime(2026, 2, 5)
            };

            var marko = new User
            {
                Id = 3,
                FirstName = "Marko",
                LastName = "Kovačević",
                DateOfBirth = new DateTime(1990, 3, 10),
                Email = "marko.kovacevic@email.com",
                PhoneNumber = "+385921112233",
                Role = UserRole.Owner,
                RegisteredAt = new DateTime(2025, 11, 1)
            };

            var petra = new User
            {
                Id = 4,
                FirstName = "Petra",
                LastName = "Babić",
                DateOfBirth = new DateTime(2000, 12, 1),
                Email = "petra.babic@email.com",
                PhoneNumber = "+385998887766",
                Role = UserRole.Guest,
                RegisteredAt = new DateTime(2026, 3, 1)
            };

            var admin = new User
            {
                Id = 5,
                FirstName = "Luka",
                LastName = "Perić",
                DateOfBirth = new DateTime(1985, 7, 20),
                Email = "luka.peric@admin.com",
                PhoneNumber = "+385915556677",
                Role = UserRole.Admin,
                RegisteredAt = new DateTime(2025, 6, 1)
            };

            var maloljetnik = new User
            {
                Id = 6,
                FirstName = "Karlo",
                LastName = "Novak",
                DateOfBirth = new DateTime(2010, 9, 25),
                Email = "karlo.novak@email.com",
                PhoneNumber = "+385912223344",
                Role = UserRole.Guest,
                RegisteredAt = new DateTime(2026, 3, 20)
            };

            var allUsers = new List<User> { ivan, ana, marko, petra, admin, maloljetnik };

            // =============================================
            // KREIRANJE LOKACIJA (VENUES) - 3 GLAVNA OBJEKTA
            // =============================================

            // --- VENUE 1: Club Culture ---
            var clubCulture = new Venue
            {
                Id = 1,
                Name = "Club Culture",
                Address = "Jabukovac 10, Zagreb",
                Latitude = 45.8144,
                Longitude = 15.9786,
                Capacity = 500,
                WorkingHours = "22:00 - 05:00",
                ContactPhone = "+38514567890",
                Description = "Najpoznatiji noćni klub u Zagrebu s vrhunskim DJ programom.",
                Type = VenueType.Club,
                ImageUrl = "https://example.com/culture.jpg"
            };

            // Stolovi za Club Culture
            var cultureTable1 = new Table { Id = 1, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, Venue = clubCulture };
            var cultureTable2 = new Table { Id = 2, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, Venue = clubCulture };
            var cultureTable3 = new Table { Id = 3, TableNumber = 3, SeatCount = 8, Zone = TableZone.VIP, Venue = clubCulture };
            var cultureTable4 = new Table { Id = 4, TableNumber = 4, SeatCount = 4, Zone = TableZone.Regular, Venue = clubCulture };
            clubCulture.Tables.AddRange(new[] { cultureTable1, cultureTable2, cultureTable3, cultureTable4 });

            // Cjenik za Club Culture
            clubCulture.PriceList.AddRange(new[]
            {
                new PriceListItem { Id = 1, ItemName = "Gin & Tonic", Price = 8.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 2, ItemName = "Vodka Red Bull", Price = 9.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 3, ItemName = "Jack & Coke", Price = 10.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 4, ItemName = "Heineken 0.5l", Price = 5.00m, Category = "Piće", Venue = clubCulture },
                new PriceListItem { Id = 5, ItemName = "VIP ulaz", Price = 30.00m, Category = "Ulaznica", Venue = clubCulture }
            });

            // Eventi za Club Culture
            var djNightCulture = new Event
            {
                Id = 1,
                Name = "Techno Night ft. MLADY",
                Description = "Najbolja techno večer u gradu s rezidentnim DJ-em.",
                StartTime = new DateTime(2026, 4, 5, 23, 0, 0),
                EndTime = new DateTime(2026, 4, 6, 5, 0, 0),
                Type = EventType.DJNight,
                EntryPrice = 15.00m,
                PosterUrl = "https://example.com/techno-night.jpg",
                AgeLimit = 18,
                Venue = clubCulture
            };

            var concertCulture = new Event
            {
                Id = 2,
                Name = "Vojko V Live",
                Description = "Vojko V dolazi u Club Culture na ekskluzivni nastup!",
                StartTime = new DateTime(2026, 4, 12, 22, 0, 0),
                EndTime = new DateTime(2026, 4, 13, 3, 0, 0),
                Type = EventType.Concert,
                EntryPrice = 25.00m,
                PosterUrl = "https://example.com/vojko-v.jpg",
                AgeLimit = 18,
                Venue = clubCulture
            };

            var pastEventCulture = new Event
            {
                Id = 3,
                Name = "Retro Party 90s",
                Description = "Vratite se u 90-te uz najbolje hitove!",
                StartTime = new DateTime(2026, 3, 15, 22, 0, 0),
                EndTime = new DateTime(2026, 3, 16, 4, 0, 0),
                Type = EventType.DJNight,
                EntryPrice = 10.00m,
                PosterUrl = "https://example.com/retro.jpg",
                AgeLimit = 18,
                Venue = clubCulture
            };

            clubCulture.Events.AddRange(new[] { djNightCulture, concertCulture, pastEventCulture });

            // --- VENUE 2: Kavana Lav ---
            var kavanaLav = new Venue
            {
                Id = 2,
                Name = "Kavana Lav",
                Address = "Ilica 45, Zagreb",
                Latitude = 45.8131,
                Longitude = 15.9665,
                Capacity = 80,
                WorkingHours = "08:00 - 23:00",
                ContactPhone = "+38511234567",
                Description = "Ugodan kafić u centru Zagreba s pub kviz večerima.",
                Type = VenueType.Cafe,
                ImageUrl = "https://example.com/lav.jpg"
            };

            var lavTable1 = new Table { Id = 5, TableNumber = 1, SeatCount = 4, Zone = TableZone.Regular, Venue = kavanaLav };
            var lavTable2 = new Table { Id = 6, TableNumber = 2, SeatCount = 6, Zone = TableZone.Regular, Venue = kavanaLav };
            var lavTable3 = new Table { Id = 7, TableNumber = 3, SeatCount = 4, Zone = TableZone.Regular, Venue = kavanaLav };
            kavanaLav.Tables.AddRange(new[] { lavTable1, lavTable2, lavTable3 });

            kavanaLav.PriceList.AddRange(new[]
            {
                new PriceListItem { Id = 6, ItemName = "Espresso", Price = 1.50m, Category = "Piće", Venue = kavanaLav },
                new PriceListItem { Id = 7, ItemName = "Cappuccino", Price = 2.50m, Category = "Piće", Venue = kavanaLav },
                new PriceListItem { Id = 8, ItemName = "Craft pivo", Price = 5.00m, Category = "Piće", Venue = kavanaLav },
                new PriceListItem { Id = 9, ItemName = "Sendvič", Price = 4.50m, Category = "Hrana", Venue = kavanaLav }
            });

            var pubQuizLav = new Event
            {
                Id = 4,
                Name = "Pub Quiz Srijeda",
                Description = "Testiraj svoje znanje svaku srijedu! Nagrada za 1. mjesto.",
                StartTime = new DateTime(2026, 4, 2, 19, 0, 0),
                EndTime = new DateTime(2026, 4, 2, 22, 0, 0),
                Type = EventType.PubQuiz,
                EntryPrice = 0.00m,
                PosterUrl = "https://example.com/pubquiz.jpg",
                AgeLimit = 0,
                Venue = kavanaLav
            };

            var acousticLav = new Event
            {
                Id = 5,
                Name = "Acoustic Night - Lokalni bendovi",
                Description = "Akustični nastupi lokalnih bendova uz craft pivo.",
                StartTime = new DateTime(2026, 4, 9, 20, 0, 0),
                EndTime = new DateTime(2026, 4, 9, 23, 0, 0),
                Type = EventType.Concert,
                EntryPrice = 5.00m,
                PosterUrl = "https://example.com/acoustic.jpg",
                AgeLimit = 0,
                Venue = kavanaLav
            };

            var pastPubQuiz = new Event
            {
                Id = 6,
                Name = "Pub Quiz - Filmska Tematika",
                Description = "Specijalni pub quiz o filmovima.",
                StartTime = new DateTime(2026, 3, 19, 19, 0, 0),
                EndTime = new DateTime(2026, 3, 19, 22, 0, 0),
                Type = EventType.PubQuiz,
                EntryPrice = 0.00m,
                PosterUrl = "https://example.com/filmquiz.jpg",
                AgeLimit = 0,
                Venue = kavanaLav
            };

            kavanaLav.Events.AddRange(new[] { pubQuizLav, acousticLav, pastPubQuiz });

            // --- VENUE 3: Park Stage ---
            var parkStage = new Venue
            {
                Id = 3,
                Name = "Park Stage Bundek",
                Address = "Bundek, Novi Zagreb",
                Latitude = 45.7869,
                Longitude = 15.9874,
                Capacity = 5000,
                WorkingHours = "16:00 - 02:00",
                ContactPhone = "+38519876543",
                Description = "Open-air pozornica pored jezera Bundek za festivale i koncerte.",
                Type = VenueType.OpenAir,
                ImageUrl = "https://example.com/bundek.jpg"
            };

            var parkTable1 = new Table { Id = 8, TableNumber = 1, SeatCount = 6, Zone = TableZone.VIP, Venue = parkStage };
            var parkTable2 = new Table { Id = 9, TableNumber = 2, SeatCount = 6, Zone = TableZone.VIP, Venue = parkStage };
            var parkTable3 = new Table { Id = 10, TableNumber = 3, SeatCount = 4, Zone = TableZone.Regular, Venue = parkStage };
            var parkTable4 = new Table { Id = 11, TableNumber = 4, SeatCount = 4, Zone = TableZone.Regular, Venue = parkStage };
            parkStage.Tables.AddRange(new[] { parkTable1, parkTable2, parkTable3, parkTable4 });

            parkStage.PriceList.AddRange(new[]
            {
                new PriceListItem { Id = 10, ItemName = "Pivo 0.5l", Price = 4.00m, Category = "Piće", Venue = parkStage },
                new PriceListItem { Id = 11, ItemName = "Kokteli", Price = 7.00m, Category = "Piće", Venue = parkStage },
                new PriceListItem { Id = 12, ItemName = "Pizza komad", Price = 3.50m, Category = "Hrana", Venue = parkStage },
                new PriceListItem { Id = 13, ItemName = "Festival pass", Price = 50.00m, Category = "Ulaznica", Venue = parkStage }
            });

            var festivalBundek = new Event
            {
                Id = 7,
                Name = "Zagreb Summer Beats",
                Description = "Dvodnevni festival elektronske glazbe na Bundeku.",
                StartTime = new DateTime(2026, 6, 20, 16, 0, 0),
                EndTime = new DateTime(2026, 6, 22, 2, 0, 0),
                Type = EventType.Festival,
                EntryPrice = 50.00m,
                PosterUrl = "https://example.com/summer-beats.jpg",
                AgeLimit = 18,
                Venue = parkStage
            };

            var concertBundek = new Event
            {
                Id = 8,
                Name = "Let 3 - Bundek Open Air",
                Description = "Legendarni Let 3 na pozornici Bundek!",
                StartTime = new DateTime(2026, 5, 10, 20, 0, 0),
                EndTime = new DateTime(2026, 5, 10, 23, 30, 0),
                Type = EventType.Concert,
                EntryPrice = 20.00m,
                PosterUrl = "https://example.com/let3.jpg",
                AgeLimit = 0,
                Venue = parkStage
            };

            var pastFestival = new Event
            {
                Id = 9,
                Name = "Spring Vibes Festival",
                Description = "Proljetni mini-festival s lokalnim DJ-evima.",
                StartTime = new DateTime(2026, 3, 22, 17, 0, 0),
                EndTime = new DateTime(2026, 3, 22, 23, 0, 0),
                Type = EventType.Festival,
                EntryPrice = 15.00m,
                PosterUrl = "https://example.com/spring-vibes.jpg",
                AgeLimit = 16,
                Venue = parkStage
            };

            parkStage.Events.AddRange(new[] { festivalBundek, concertBundek, pastFestival });

            var allVenues = new List<Venue> { clubCulture, kavanaLav, parkStage };
            var allEvents = new List<Event> { djNightCulture, concertCulture, pastEventCulture, pubQuizLav, acousticLav, pastPubQuiz, festivalBundek, concertBundek, pastFestival };

            // =============================================
            // KREIRANJE REZERVACIJA
            // =============================================
            var reservation1 = new Reservation
            {
                Id = 1,
                CreatedAt = new DateTime(2026, 3, 28),
                NumberOfGuests = 4,
                Status = ReservationStatus.Confirmed,
                Note = "Rođendan, molimo balon dekoraciju",
                MinimumSpending = 100.00m,
                User = ivan,
                Table = cultureTable2,
                Event = djNightCulture
            };

            var reservation2 = new Reservation
            {
                Id = 2,
                CreatedAt = new DateTime(2026, 3, 29),
                NumberOfGuests = 6,
                Status = ReservationStatus.Pending,
                Note = "",
                MinimumSpending = 150.00m,
                User = ana,
                Table = cultureTable3,
                Event = concertCulture
            };

            var reservation3 = new Reservation
            {
                Id = 3,
                CreatedAt = new DateTime(2026, 3, 30),
                NumberOfGuests = 3,
                Status = ReservationStatus.Confirmed,
                Note = "Blizu pozornice ako je moguće",
                MinimumSpending = 0.00m,
                User = petra,
                Table = lavTable1,
                Event = pubQuizLav
            };

            var reservation4 = new Reservation
            {
                Id = 4,
                CreatedAt = new DateTime(2026, 3, 25),
                NumberOfGuests = 5,
                Status = ReservationStatus.Cancelled,
                Note = "Otkazano zbog bolesti",
                MinimumSpending = 200.00m,
                User = ivan,
                Table = parkTable1,
                Event = festivalBundek
            };

            var reservation5 = new Reservation
            {
                Id = 5,
                CreatedAt = new DateTime(2026, 3, 31),
                NumberOfGuests = 4,
                Status = ReservationStatus.Confirmed,
                Note = "",
                MinimumSpending = 0.00m,
                User = ana,
                Table = parkTable3,
                Event = concertBundek
            };

            // Dodaj rezervacije u evente i korisnike
            djNightCulture.Reservations.Add(reservation1);
            concertCulture.Reservations.Add(reservation2);
            pubQuizLav.Reservations.Add(reservation3);
            festivalBundek.Reservations.Add(reservation4);
            concertBundek.Reservations.Add(reservation5);

            ivan.Reservations.AddRange(new[] { reservation1, reservation4 });
            ana.Reservations.AddRange(new[] { reservation2, reservation5 });
            petra.Reservations.Add(reservation3);

            var allReservations = new List<Reservation> { reservation1, reservation2, reservation3, reservation4, reservation5 };

            // =============================================
            // KREIRANJE RECENZIJA
            // =============================================
            var review1 = new Review
            {
                Id = 1,
                Rating = 5,
                Comment = "Odlična atmosfera, DJ je bio fenomenalan!",
                CreatedAt = new DateTime(2026, 3, 16),
                User = ivan,
                Event = pastEventCulture
            };

            var review2 = new Review
            {
                Id = 2,
                Rating = 4,
                Comment = "Super quiz, pitanja su bila zanimljiva.",
                CreatedAt = new DateTime(2026, 3, 20),
                User = ana,
                Event = pastPubQuiz
            };

            var review3 = new Review
            {
                Id = 3,
                Rating = 3,
                Comment = "Bilo je OK, ali predugo čekanje za piće.",
                CreatedAt = new DateTime(2026, 3, 16),
                User = petra,
                Event = pastEventCulture
            };

            var review4 = new Review
            {
                Id = 4,
                Rating = 5,
                Comment = "Najbolji festival ove godine, 10/10!",
                CreatedAt = new DateTime(2026, 3, 23),
                User = ivan,
                Event = pastFestival
            };

            var review5 = new Review
            {
                Id = 5,
                Rating = 4,
                Comment = "Dobar vibe, lokacija predivna.",
                CreatedAt = new DateTime(2026, 3, 23),
                User = marko,
                Event = pastFestival
            };

            pastEventCulture.Reviews.AddRange(new[] { review1, review3 });
            pastPubQuiz.Reviews.Add(review2);
            pastFestival.Reviews.AddRange(new[] { review4, review5 });

            ivan.Reviews.AddRange(new[] { review1, review4 });
            ana.Reviews.Add(review2);
            petra.Reviews.Add(review3);
            marko.Reviews.Add(review5);

            // Favorites (N-N veza)
            ivan.FavoriteVenues.AddRange(new[] { clubCulture, parkStage });
            ana.FavoriteVenues.Add(kavanaLav);
            petra.FavoriteVenues.AddRange(new[] { kavanaLav, clubCulture });

            // =============================================
            // LINQ UPITI
            // =============================================

            Console.WriteLine("=== LINQ UPITI ZA ZAGREB EVENTS ===\n");

            // 1. Svi nadolazeci eventi, sortirani po datumu
            Console.WriteLine("1. Nadolazeći eventi (sortirani po datumu):");
            var upcomingEvents = allEvents
                .Where(e => e.StartTime > DateTime.Now)
                .OrderBy(e => e.StartTime)
                .ToList();

            foreach (var e in upcomingEvents)
                Console.WriteLine($"   {e.StartTime:dd.MM.yyyy HH:mm} - {e.Name} @ {e.Venue.Name} ({e.Type})");

            // 2. Besplatni eventi
            Console.WriteLine("\n2. Besplatni eventi:");
            var freeEvents = allEvents
                .Where(e => e.EntryPrice == 0)
                .ToList();

            foreach (var e in freeEvents)
                Console.WriteLine($"   {e.Name} @ {e.Venue.Name}");

            // 3. Klubovi s VIP stolovima
            Console.WriteLine("\n3. Lokacije s VIP stolovima:");
            var venuesWithVip = allVenues
                .Where(v => v.Tables.Any(t => t.Zone == TableZone.VIP))
                .ToList();

            foreach (var v in venuesWithVip)
            {
                int vipCount = v.Tables.Count(t => t.Zone == TableZone.VIP);
                Console.WriteLine($"   {v.Name} - {vipCount} VIP stol(ova)");
            }

            // 4. Prosjecna ocjena po eventu koji ima recenzije
            Console.WriteLine("\n4. Prosječna ocjena evenata:");
            var ratedEvents = allEvents
                .Where(e => e.Reviews.Any())
                .OrderByDescending(e => e.AverageRating)
                .ToList();

            foreach (var e in ratedEvents)
                Console.WriteLine($"   {e.Name} - {e.AverageRating:F1}/5 ({e.Reviews.Count} recenzija)");

            // 5. Korisnici koji imaju potvrdjene rezervacije
            Console.WriteLine("\n5. Korisnici s potvrđenim rezervacijama:");
            var usersWithConfirmed = allUsers
                .Where(u => u.Reservations.Any(r => r.Status == ReservationStatus.Confirmed))
                .ToList();

            foreach (var u in usersWithConfirmed)
            {
                int count = u.Reservations.Count(r => r.Status == ReservationStatus.Confirmed);
                Console.WriteLine($"   {u.FullName} - {count} potvrđena rezervacija");
            }

            // 6. Ukupan broj evenata po tipu
            Console.WriteLine("\n6. Broj evenata po tipu:");
            var eventsByType = allEvents
                .GroupBy(e => e.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            foreach (var g in eventsByType)
                Console.WriteLine($"   {g.Type}: {g.Count}");

            // 7. Top 3 najskuplja pica u svim lokacijama
            Console.WriteLine("\n7. Top 3 najskuplja pića:");
            var topDrinks = allVenues
                .SelectMany(v => v.PriceList)
                .Where(p => p.Category == "Piće")
                .OrderByDescending(p => p.Price)
                .Take(3)
                .ToList();

            foreach (var d in topDrinks)
                Console.WriteLine($"   {d.ItemName} - {d.Price:F2} EUR @ {d.Venue.Name}");

            // 8. Punoljetni korisnici koji mogu na 18+ evente
            Console.WriteLine("\n8. Punoljetni korisnici:");
            var adultUsers = allUsers
                .Where(u => u.IsAdult)
                .OrderBy(u => u.Age)
                .ToList();

            foreach (var u in adultUsers)
                Console.WriteLine($"   {u.FullName} ({u.Age} god.)");

            // 9. Lokacija s najvise kapaciteta
            Console.WriteLine("\n9. Lokacija s najvećim kapacitetom:");
            var biggestVenue = allVenues
                .OrderByDescending(v => v.Capacity)
                .First();

            Console.WriteLine($"   {biggestVenue.Name} - kapacitet: {biggestVenue.Capacity}");

            // 10. Eventi za odredjenu lokaciju (podupit) - svi eventi u klubovima
            Console.WriteLine("\n10. Svi eventi u klubovima (VenueType.Club):");
            var clubEvents = allEvents
                .Where(e => e.Venue.Type == VenueType.Club)
                .ToList();

            foreach (var e in clubEvents)
                Console.WriteLine($"   {e.Name} - {e.StartTime:dd.MM.yyyy}");

            // 11. Korisnici koji su ocjenili dogadjaj s 5 zvjezdica
            Console.WriteLine("\n11. Korisnici koji su dali ocjenu 5:");
            var fiveStarReviewers = allUsers
                .Where(u => u.Reviews.Any(r => r.Rating == 5))
                .Select(u => new { u.FullName, EventNames = u.Reviews.Where(r => r.Rating == 5).Select(r => r.Event.Name) })
                .ToList();

            foreach (var u in fiveStarReviewers)
                Console.WriteLine($"   {u.FullName} -> {string.Join(", ", u.EventNames)}");

            // 12. Ukupna minimalna potrosnja svih aktivnih rezervacija
            Console.WriteLine("\n12. Ukupna minimalna potrošnja (potvrđene rezervacije):");
            var totalMinSpending = allReservations
                .Where(r => r.Status == ReservationStatus.Confirmed)
                .Sum(r => r.MinimumSpending);

            Console.WriteLine($"   {totalMinSpending:F2} EUR");

            // 13. Lokacije koje korisnik Ivan ima u favoritima - s brojem evenata
            Console.WriteLine("\n13. Ivanove omiljene lokacije i broj evenata:");
            var ivanFavorites = ivan.FavoriteVenues
                .Select(v => new { v.Name, EventCount = v.Events.Count })
                .ToList();

            foreach (var f in ivanFavorites)
                Console.WriteLine($"   {f.Name} - {f.EventCount} evenata");

            // 14. Prosjecna cijena ulaza po tipu lokacije
            Console.WriteLine("\n14. Prosječna cijena ulaza po tipu lokacije:");
            var avgPriceByVenueType = allEvents
                .GroupBy(e => e.Venue.Type)
                .Select(g => new { VenueType = g.Key, AvgPrice = g.Average(e => e.EntryPrice) })
                .OrderByDescending(x => x.AvgPrice)
                .ToList();

            foreach (var item in avgPriceByVenueType)
                Console.WriteLine($"   {item.VenueType}: {item.AvgPrice:F2} EUR");

            // 15. Eventi s vise od 0 rezervacija, s brojem gostiju
            Console.WriteLine("\n15. Eventi s rezervacijama (ukupan broj gostiju):");
            var eventsWithReservations = allEvents
                .Where(e => e.Reservations.Any())
                .Select(e => new
                {
                    e.Name,
                    TotalGuests = e.Reservations
                        .Where(r => r.Status != ReservationStatus.Cancelled)
                        .Sum(r => r.NumberOfGuests)
                })
                .OrderByDescending(x => x.TotalGuests)
                .ToList();

            foreach (var e in eventsWithReservations)
                Console.WriteLine($"   {e.Name} - {e.TotalGuests} gostiju");

            Console.WriteLine("\n=== KRAJ ===");
        }
    }
}
