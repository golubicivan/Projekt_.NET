using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Tests
{
    public class VenueApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        public VenueApiTests(CustomWebApplicationFactory factory) => _factory = factory;

        private HttpClient AdminClient()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-Roles", "Admin");
            return client;
        }

        private int SeedVenue()
        {
            var db = _factory.CreateDbContext();
            var v = new Venue { Name = "V " + Guid.NewGuid().ToString("N")[..6], Address = "Adr" };
            db.Venues.Add(v);
            db.SaveChanges();
            return v.Id;
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            SeedVenue();
            var response = await _factory.CreateClient().GetAsync("/api/venues");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var list = await response.Content.ReadFromJsonAsync<List<VenueDto>>();
            list.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ReturnsVenue_WhenExists()
        {
            var id = SeedVenue();
            var response = await _factory.CreateClient().GetAsync($"/api/venues/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var dto = await response.Content.ReadFromJsonAsync<VenueDto>();
            dto!.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetById_Returns404_WhenNotExists()
        {
            var response = await _factory.CreateClient().GetAsync("/api/venues/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Returns201_WhenValid()
        {
            var client = AdminClient();
            var dto = new VenueCreateDto { Name = "Novi venue", Address = "Ilica 1", Capacity = 100, Type = 0 };
            var response = await client.PostAsJsonAsync("/api/venues", dto);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var created = await response.Content.ReadFromJsonAsync<VenueDto>();
            created!.Name.Should().Be("Novi venue");
        }

        [Fact]
        public async Task Create_Returns401_WhenNotAuthenticated()
        {
            var client = _factory.CreateClient();
            var dto = new VenueCreateDto { Name = "X", Type = 0 };
            var response = await client.PostAsJsonAsync("/api/venues", dto);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenExists()
        {
            var id = SeedVenue();
            var client = AdminClient();
            var dto = new VenueCreateDto { Name = "Promijenjeno ime", Address = "Nova adresa", Capacity = 50, Type = 1 };
            var response = await client.PutAsJsonAsync($"/api/venues/{id}", dto);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var updated = await response.Content.ReadFromJsonAsync<VenueDto>();
            updated!.Name.Should().Be("Promijenjeno ime");
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenExists()
        {
            var id = SeedVenue();
            var client = AdminClient();
            var response = await client.DeleteAsync($"/api/venues/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Returns404_WhenNotExists()
        {
            var client = AdminClient();
            var response = await client.DeleteAsync("/api/venues/999999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
