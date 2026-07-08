using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace ZagrebEvents.PlaywrightTests
{
    // ============================================================
    // E2E scenarij od 10 koraka kroz UI (Chromium, headless).
    // Pokriva: kartu, welcome popup, global search, detalje lokacije,
    // detalje eventa, tlocrt, login, admin stranice, odjavu.
    // ============================================================
    [TestFixture]
    public class UiScenarioTests : PageTest
    {
        private static readonly string BaseUrl =
            Environment.GetEnvironmentVariable("ZE_BASEURL") ?? "http://localhost:5199";

        [SetUp]
        public async Task CheckAppAlive()
        {
            try
            {
                var ctx = await Playwright.APIRequest.NewContextAsync(new() { BaseURL = BaseUrl });
                var ping = await ctx.GetAsync("/");
                await ctx.DisposeAsync();
                if (!ping.Ok) Assert.Ignore($"App na {BaseUrl} ne odgovara.");
            }
            catch { Assert.Ignore($"App na {BaseUrl} nije pokrenut. Pokreni run-playwright.ps1."); }
        }

        [Test]
        public async Task Scenarij_10_koraka_kroz_aplikaciju()
        {
            // ---- KORAK 1: Otvori kartu (pocetna) ----
            await Page.GotoAsync(BaseUrl + "/");
            await Expect(Page).ToHaveTitleAsync(new Regex("GdjeCemo"));

            // ---- KORAK 2: Welcome popup -> "Prikazi kartu" (ako je prikazan) ----
            var welcomeBtn = Page.Locator("#welcomeGo");
            if (await welcomeBtn.CountAsync() > 0 && await welcomeBtn.IsVisibleAsync())
                await welcomeBtn.ClickAsync();
            // pinovi su se ucitali (klasteri ili pojedinacni pinovi)
            await Expect(Page.Locator(".venue-pin, .ze-cluster").First).ToBeVisibleAsync(new() { Timeout = 10000 });

            // ---- KORAK 3: Global search "h2o" iz navbara ----
            await Page.FillAsync(".gs-nav-input", "h2o");
            await Page.Keyboard.PressAsync("Enter");
            await Expect(Page).ToHaveURLAsync(new Regex("/trazi"));
            await Expect(Page.Locator(".gs-hit-title", new() { HasTextString = "Club H2O" }).First).ToBeVisibleAsync();

            // ---- KORAK 4: Otvori detalje lokacije Club H2O ----
            await Page.Locator(".gs-hit", new() { HasTextString = "Club H2O" }).First.ClickAsync();
            await Expect(Page.Locator("h1", new() { HasTextString = "Club H2O" })).ToBeVisibleAsync();
            await Expect(Page.Locator("h2", new() { HasTextString = "O lokaciji" })).ToBeVisibleAsync();

            // ---- KORAK 5: Otvori detalje eventa s te lokacije ----
            await Page.Locator(".venue-event-row a").First.ClickAsync();
            await Expect(Page.Locator("h1.event-detail-title")).ToBeVisibleAsync();

            // ---- KORAK 6: Tlocrt & zauzetost modal (otvori pa zatvori) ----
            await Page.ClickAsync("#floorplanBtn");
            await Expect(Page.Locator(".floorplan-card")).ToBeVisibleAsync();
            await Expect(Page.Locator(".fp-chip").First).ToBeVisibleAsync(); // pločice stolova
            await Page.ClickAsync("#floorplanClose");
            await Expect(Page.Locator(".floorplan-card")).ToBeHiddenAsync();

            // ---- KORAK 7: Prijava kao admin ----
            await Page.GotoAsync(BaseUrl + "/prijava");
            await Page.FillAsync("input[name=email]", "luka.peric@admin.com");
            await Page.FillAsync("input[name=password]", "admin123");
            await Page.ClickAsync("button[type=submit]");
            await Expect(Page).ToHaveURLAsync(new Regex(BaseUrl.Replace("http://", "") + "/?$"));

            // ---- KORAK 8: Admin - stranica svih rezervacija ----
            await Page.GotoAsync(BaseUrl + "/rezervacije");
            await Expect(Page.Locator("h1", new() { HasTextString = "ezervacij" })).ToBeVisibleAsync();

            // ---- KORAK 9: Admin - popis korisnika ----
            await Page.GotoAsync(BaseUrl + "/korisnici");
            await Expect(Page.Locator("body")).ToContainTextAsync("Luka");

            // ---- KORAK 10: Odjava -> gost opet vidi Prijavu ----
            await Page.GotoAsync(BaseUrl + "/odjava");
            await Page.GotoAsync(BaseUrl + "/prijava");
            await Expect(Page.Locator("input[name=email]")).ToBeVisibleAsync();
        }
    }
}
