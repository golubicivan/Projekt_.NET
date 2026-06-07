using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Tests
{
    public class EventApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public EventApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        // HttpClient s admin ovlastima (šalje X-Test-Roles header)
        private HttpClient AdminClient()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-Roles", "Admin");
            return client;
        }

        // Pomoćna: kreiraj venue + event direktno u bazi
        private (int venueId, int eventId) SeedEvent()
        {
            var db = _factory.CreateDbContext();
            var venue = new Venue { Name = "Test Venue " + Guid.NewGuid().ToString("N")[..6], Address = "Test 1" };
            db.Venues.Add(venue);
            db.SaveChanges();

            var ev = new Event
            {
                Name = "Test Event " + Guid.NewGuid().ToString("N")[..6],
                Description = "Opis",
                StartTime = DateTime.Now.AddDays(5),
                EndTime = DateTime.Now.AddDays(5).AddHours(3),
                Type = EventType.Concert,
                EntryPrice = 10,
                VenueId = venue.Id
            };
            db.Events.Add(ev);
            db.SaveChanges();
            return (venue.Id, ev.Id);
        }

        private int SeedVenue()
        {
            var db = _factory.CreateDbContext();
            var venue = new Venue { Name = "Venue " + Guid.NewGuid().ToString("N")[..6], Address = "Adr" };
            db.Venues.Add(venue);
            db.SaveChanges();
            return venue.Id;
        }

        // ===================== GET ALL =====================
        [Fact]
        public async Task GetAll_ReturnsOkAndCollection()
        {
            SeedEvent();
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/events");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var list = await response.Content.ReadFromJsonAsync<List<EventDto>>();
            list.Should().NotBeNull();
            list!.Should().NotBeEmpty();
        }

        // ===================== GET BY ID (postoji) =====================
        [Fact]
        public async Task GetById_ReturnsEvent_WhenExists()
        {
            var (_, eventId) = SeedEvent();
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/events/{eventId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var dto = await response.Content.ReadFromJsonAsync<EventDto>();
            dto.Should().NotBeNull();
            dto!.Id.Should().Be(eventId);
            dto.Venue.Should().NotBeNull(); // ugniježđeni DTO
        }

        // ===================== GET BY ID (ne postoji) =====================
        [Fact]
        public async Task GetById_Returns404_WhenNotExists()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/events/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // ===================== POST (uspjeh) =====================
        [Fact]
        public async Task Create_Returns201_WhenValid()
        {
            var venueId = SeedVenue();
            var client = AdminClient();

            var dto = new EventCreateDto
            {
                Name = "Novi API event",
                Description = "Opis",
                StartTime = DateTime.Now.AddDays(10),
                EndTime = DateTime.Now.AddDays(10).AddHours(4),
                Type = 1,
                EntryPrice = 20,
                AgeLimit = 18,
                VenueId = venueId
            };

            var response = await client.PostAsJsonAsync("/api/events", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var created = await response.Content.ReadFromJsonAsync<EventDto>();
            created!.Name.Should().Be("Novi API event");
            created.Id.Should().BeGreaterThan(0);
        }

        // ===================== POST (validacijska greška) =====================
        [Fact]
        public async Task Create_Returns400_WhenInvalid()
        {
            var venueId = SeedVenue();
            var client = AdminClient();

            // Name je obavezan -> prazan ime + kraj prije početka
            var dto = new EventCreateDto
            {
                Name = "",
                StartTime = DateTime.Now.AddDays(10),
                EndTime = DateTime.Now.AddDays(9),
                VenueId = venueId
            };

            var response = await client.PostAsJsonAsync("/api/events", dto);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // ===================== POST (bez auth -> 401) =====================
        [Fact]
        public async Task Create_Returns401_WhenNotAuthenticated()
        {
            var venueId = SeedVenue();
            var client = _factory.CreateClient(); // bez X-Test-Roles

            var dto = new EventCreateDto
            {
                Name = "X",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(1),
                VenueId = venueId
            };

            var response = await client.PostAsJsonAsync("/api/events", dto);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        // ===================== PUT (uspjeh) =====================
        [Fact]
        public async Task Update_ReturnsOk_WhenExists()
        {
            var (venueId, eventId) = SeedEvent();
            var client = AdminClient();

            var dto = new EventCreateDto
            {
                Name = "Ažurirani naziv",
                Description = "Novi opis",
                StartTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(3).AddHours(2),
                Type = 0,
                EntryPrice = 5,
                VenueId = venueId
            };

            var response = await client.PutAsJsonAsync($"/api/events/{eventId}", dto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var updated = await response.Content.ReadFromJsonAsync<EventDto>();
            updated!.Name.Should().Be("Ažurirani naziv");
        }

        // ===================== PUT (ne postoji) =====================
        [Fact]
        public async Task Update_Returns404_WhenNotExists()
        {
            var venueId = SeedVenue();
            var client = AdminClient();

            var dto = new EventCreateDto
            {
                Name = "X",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(1),
                VenueId = venueId
            };

            var response = await client.PutAsJsonAsync("/api/events/999999", dto);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // ===================== DELETE (uspjeh) =====================
        [Fact]
        public async Task Delete_ReturnsNoContent_WhenExists()
        {
            var (_, eventId) = SeedEvent();
            var client = AdminClient();

            var response = await client.DeleteAsync($"/api/events/{eventId}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Nakon soft delete više se ne dohvaća
            var getResponse = await client.GetAsync($"/api/events/{eventId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // ===================== DELETE (ne postoji) =====================
        [Fact]
        public async Task Delete_Returns404_WhenNotExists()
        {
            var client = AdminClient();
            var response = await client.DeleteAsync("/api/events/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
