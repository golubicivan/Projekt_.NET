using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace ZagrebEvents.PlaywrightTests
{
    // ============================================================
    // Playwright API testovi za SVE API endpointe (CRUD).
    // Pokretanje: lab5/run-playwright.ps1 (digne app na 5199 pa vrti testove)
    // ili rucno: pokreni app pa  dotnet test ZagrebEvents.PlaywrightTests
    // Ako app nije dostupan na BASEURL, testovi se preskacu (Ignore).
    // ============================================================
    [TestFixture]
    public class ApiEndpointTests
    {
        private static readonly string BaseUrl =
            Environment.GetEnvironmentVariable("ZE_BASEURL") ?? "http://localhost:5199";

        private IPlaywright _pw = null!;
        private IAPIRequestContext _admin = null!;   // s admin cookiejem
        private IAPIRequestContext _anon = null!;    // bez prijave

        // id-evi kreirani u testovima (za cleanup i chaining)
        private int _venueId, _eventId, _tableId, _priceId, _reviewId, _reservationId;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _pw = await Playwright.CreateAsync();
            _anon = await _pw.APIRequest.NewContextAsync(new() { BaseURL = BaseUrl, IgnoreHTTPSErrors = true });

            // Je li app uopce pokrenut?
            try
            {
                var ping = await _anon.GetAsync("/api/events");
                if (!ping.Ok) Assert.Ignore($"App na {BaseUrl} ne odgovara ({ping.Status}).");
            }
            catch (Exception e)
            {
                Assert.Ignore($"App na {BaseUrl} nije pokrenut ({e.Message}). Pokreni run-playwright.ps1.");
            }

            // Admin login preko MVC forme (cookie ostaje u kontekstu)
            _admin = await _pw.APIRequest.NewContextAsync(new() { BaseURL = BaseUrl, IgnoreHTTPSErrors = true });
            var loginPage = await _admin.GetAsync("/prijava");
            var html = await loginPage.TextAsync();
            var token = Regex.Match(html, "RequestVerificationToken\"[^>]*value=\"([^\"]+)\"").Groups[1].Value;
            var body = $"email={Uri.EscapeDataString("luka.peric@admin.com")}" +
                       $"&password={Uri.EscapeDataString("admin123")}" +
                       $"&__RequestVerificationToken={Uri.EscapeDataString(token)}";
            var login = await _admin.PostAsync("/prijava", new()
            {
                Data = body,
                Headers = new Dictionary<string, string> { ["Content-Type"] = "application/x-www-form-urlencoded" }
            });
            Assert.That(login.Status, Is.LessThan(400), "Admin login nije uspio.");
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            // best-effort cleanup (redoslijed zbog FK)
            if (_admin != null)
            {
                if (_reservationId > 0) await _admin.DeleteAsync($"/api/reservations/{_reservationId}");
                if (_reviewId > 0) await _admin.DeleteAsync($"/api/reviews/{_reviewId}");
                if (_eventId > 0) await _admin.DeleteAsync($"/api/events/{_eventId}");
                if (_priceId > 0) await _admin.DeleteAsync($"/api/pricelistitems/{_priceId}");
                if (_tableId > 0) await _admin.DeleteAsync($"/api/tables/{_tableId}");
                if (_venueId > 0) await _admin.DeleteAsync($"/api/venues/{_venueId}");
                await _admin.DisposeAsync();
            }
            if (_anon != null) await _anon.DisposeAsync();
            _pw?.Dispose();
        }

        private static async Task<JsonElement> Json(IAPIResponse r) =>
            JsonDocument.Parse(await r.TextAsync()).RootElement;

        // ---------- VENUES ----------
        [Test, Order(1)]
        public async Task Venues_FullCrud()
        {
            (await _anon.GetAsync("/api/venues")).Status.Should(200, "GET all venues");
            (await _anon.GetAsync("/api/venues/3")).Status.Should(200, "GET venue by id");
            (await _anon.GetAsync("/api/venues/999999")).Status.Should(404, "GET venue 404");
            (await _anon.GetAsync("/api/venues?q=h2o")).Status.Should(200, "GET venues search");

            // POST bez prijave -> 401
            (await _anon.PostAsync("/api/venues", Payload(new { name = "X", address = "Y" })))
                .Status.Should(401, "POST venue anon");

            var create = await _admin.PostAsync("/api/venues", Payload(new
            {
                name = $"PW Venue {Guid.NewGuid():N}",
                address = "Playwright 1",
                latitude = 45.81,
                longitude = 15.98,
                capacity = 50,
                workingHours = "20-04",
                contactPhone = "+385",
                description = "pw test",
                type = 0,
                imageUrl = "",
                logoUrl = ""
            }));
            create.Status.Should(201, "POST venue");
            _venueId = (await Json(create)).GetProperty("id").GetInt32();

            var put = await _admin.PutAsync($"/api/venues/{_venueId}", Payload(new
            {
                name = "PW Venue v2",
                address = "Playwright 2",
                latitude = 45.81,
                longitude = 15.98,
                capacity = 60,
                workingHours = "20-04",
                contactPhone = "+385",
                description = "pw test v2",
                type = 1,
                imageUrl = "",
                logoUrl = ""
            }));
            put.Status.Should(200, "PUT venue");
            (await _admin.PutAsync("/api/venues/999999", Payload(new { name = "x", address = "y" })))
                .Status.Should(404, "PUT venue 404");
        }

        // ---------- TABLES ----------
        [Test, Order(2)]
        public async Task Tables_FullCrud()
        {
            (await _anon.GetAsync("/api/tables")).Status.Should(200, "GET all tables");
            (await _anon.GetAsync("/api/tables/999999")).Status.Should(404, "GET table 404");

            var create = await _admin.PostAsync("/api/tables", Payload(new
            { tableNumber = 77, seatCount = 4, zone = 1, venueId = _venueId }));
            create.Status.Should(201, "POST table");
            _tableId = (await Json(create)).GetProperty("id").GetInt32();

            (await _anon.GetAsync($"/api/tables/{_tableId}")).Status.Should(200, "GET table by id");
            (await _admin.PutAsync($"/api/tables/{_tableId}", Payload(new
            { tableNumber = 78, seatCount = 6, zone = 0, venueId = _venueId })))
                .Status.Should(200, "PUT table");
        }

        // ---------- EVENTS ----------
        [Test, Order(3)]
        public async Task Events_FullCrud()
        {
            (await _anon.GetAsync("/api/events")).Status.Should(200, "GET all events");
            (await _anon.GetAsync("/api/events?q=h2o")).Status.Should(200, "GET events search");
            (await _anon.GetAsync("/api/events/999999")).Status.Should(404, "GET event 404");
            (await _anon.PostAsync("/api/events", Payload(new { name = "X" }))).Status.Should(401, "POST event anon");

            var create = await _admin.PostAsync("/api/events", Payload(new
            {
                name = $"PW Event {Guid.NewGuid():N}",
                description = "pw",
                startTime = DateTime.Now.AddDays(30),
                endTime = DateTime.Now.AddDays(30).AddHours(6),
                type = 0,
                entryPrice = 12.5,
                posterUrl = "",
                ageLimit = 18,
                venueId = _venueId
            }));
            create.Status.Should(201, "POST event");
            _eventId = (await Json(create)).GetProperty("id").GetInt32();

            (await _anon.GetAsync($"/api/events/{_eventId}")).Status.Should(200, "GET event by id");

            // validacija: kraj prije pocetka -> 400
            (await _admin.PostAsync("/api/events", Payload(new
            {
                name = "PW invalid",
                description = "pw",
                startTime = DateTime.Now.AddDays(31),
                endTime = DateTime.Now.AddDays(30),
                type = 0,
                entryPrice = 1,
                posterUrl = "",
                ageLimit = 0,
                venueId = _venueId
            }))).Status.Should(400, "POST event validacija");

            (await _admin.PutAsync($"/api/events/{_eventId}", Payload(new
            {
                name = "PW Event v2",
                description = "pw v2",
                startTime = DateTime.Now.AddDays(30),
                endTime = DateTime.Now.AddDays(30).AddHours(5),
                type = 1,
                entryPrice = 15,
                posterUrl = "",
                ageLimit = 18,
                venueId = _venueId
            }))).Status.Should(200, "PUT event");
        }

        // ---------- PRICELIST ----------
        [Test, Order(4)]
        public async Task PriceListItems_FullCrud()
        {
            (await _anon.GetAsync("/api/pricelistitems")).Status.Should(200, "GET all pricelist");
            (await _anon.GetAsync("/api/pricelistitems/999999")).Status.Should(404, "GET pricelist 404");

            var create = await _admin.PostAsync("/api/pricelistitems", Payload(new
            { itemName = "PW pivo", price = 4.5, category = "Pice", venueId = _venueId }));
            create.Status.Should(201, "POST pricelist");
            _priceId = (await Json(create)).GetProperty("id").GetInt32();

            (await _anon.GetAsync($"/api/pricelistitems/{_priceId}")).Status.Should(200, "GET pricelist by id");
            (await _admin.PutAsync($"/api/pricelistitems/{_priceId}", Payload(new
            { itemName = "PW pivo v2", price = 5.0, category = "Pice", venueId = _venueId })))
                .Status.Should(200, "PUT pricelist");
        }

        // ---------- REVIEWS ----------
        [Test, Order(5)]
        public async Task Reviews_FullCrud()
        {
            (await _anon.GetAsync("/api/reviews")).Status.Should(200, "GET all reviews");
            (await _anon.GetAsync("/api/reviews/999999")).Status.Should(404, "GET review 404");
            (await _anon.PostAsync("/api/reviews", Payload(new { rating = 5 }))).Status.Should(401, "POST review anon");

            var create = await _admin.PostAsync("/api/reviews", Payload(new
            { rating = 4, comment = "PW recenzija", userId = 5, eventId = _eventId }));
            create.Status.Should(201, "POST review");
            _reviewId = (await Json(create)).GetProperty("id").GetInt32();

            (await _anon.GetAsync($"/api/reviews/{_reviewId}")).Status.Should(200, "GET review by id");
            (await _admin.PutAsync($"/api/reviews/{_reviewId}", Payload(new
            { rating = 3, comment = "PW recenzija v2", userId = 5, eventId = _eventId })))
                .Status.Should(200, "PUT review");
        }

        // ---------- RESERVATIONS ----------
        [Test, Order(6)]
        public async Task Reservations_FullCrud()
        {
            (await _admin.GetAsync("/api/reservations")).Status.Should(200, "GET all reservations (admin)");
            (await _admin.GetAsync("/api/reservations/999999")).Status.Should(404, "GET reservation 404");

            var create = await _admin.PostAsync("/api/reservations", Payload(new
            { numberOfGuests = 2, note = "PW", status = 0, userId = 5, eventId = _eventId, tableId = _tableId }));
            create.Status.Should(201, "POST reservation");
            _reservationId = (await Json(create)).GetProperty("id").GetInt32();

            (await _admin.GetAsync($"/api/reservations/{_reservationId}")).Status.Should(200, "GET reservation by id");
            (await _admin.PutAsync($"/api/reservations/{_reservationId}", Payload(new
            { numberOfGuests = 3, note = "PW v2", status = 1, userId = 5, eventId = _eventId, tableId = _tableId })))
                .Status.Should(200, "PUT reservation");
        }

        // ---------- USERS ----------
        [Test, Order(7)]
        public async Task Users_ReadEndpoints()
        {
            (await _admin.GetAsync("/api/users")).Status.Should(200, "GET all users (admin)");
            (await _admin.GetAsync("/api/users/1")).Status.Should(200, "GET user by id");
            (await _admin.GetAsync("/api/users/999999")).Status.Should(404, "GET user 404");
            (await _anon.GetAsync("/api/users")).Status.Should(401, "GET users anon -> 401");
        }

        // ---------- DELETE lanac (redoslijed zbog FK) ----------
        [Test, Order(8)]
        public async Task Deletes_AllCreatedEntities()
        {
            (await _admin.DeleteAsync($"/api/reservations/{_reservationId}")).Status.Should(204, "DELETE reservation");
            (await _admin.DeleteAsync($"/api/reviews/{_reviewId}")).Status.Should(204, "DELETE review");
            (await _admin.DeleteAsync($"/api/events/{_eventId}")).Status.Should(204, "DELETE event");
            (await _admin.DeleteAsync($"/api/pricelistitems/{_priceId}")).Status.Should(204, "DELETE pricelist");
            (await _admin.DeleteAsync($"/api/tables/{_tableId}")).Status.Should(204, "DELETE table");
            (await _admin.DeleteAsync($"/api/venues/{_venueId}")).Status.Should(204, "DELETE venue");
            (await _admin.DeleteAsync("/api/events/999999")).Status.Should(404, "DELETE event 404");
            _reservationId = _reviewId = _eventId = _priceId = _tableId = _venueId = 0;
        }

        private static APIRequestContextOptions Payload(object o) => new()
        {
            DataObject = o,
            Headers = new Dictionary<string, string> { ["Content-Type"] = "application/json" }
        };
    }

    internal static class AssertExt
    {
        public static void Should(this int actual, int expected, string what) =>
            Assert.That(actual, Is.EqualTo(expected), what);
    }
}
