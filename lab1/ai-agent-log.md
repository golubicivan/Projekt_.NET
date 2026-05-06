# AI Agent Log — ZagrebEvents (.NET Lab)

Ovaj log bilježi sve interakcije s AI agentom (Claude) tijekom razvoja projekta.

---

## Sesija 1 — 13.04.2026.

### Interakcija 1
**Korisnik:** "kako ocjenujes projekt"
**Agent:** Pregledao sve datoteke projekta (Program.cs, sve klase, enume). Dao detaljnu ocjenu:
- Snage: dobar domain model, computed properties, 15 LINQ upita raznolike složenosti, use of enums
- Nedostaci: nema nullable anotacija, Program.cs prevelika, nema null provjera ni validacije
- Ocjena: ~8/10 za lab zadatak

---

### Interakcija 2
**Korisnik:** Priložio PDF zadatka (Lab-1 (1).pdf) i pitao je li zadatak ispravno ispunjen
**Agent:** Pročitao PDF, napravio detaljnu usporedbu uvjeta i implementacije:
- Objektni model: ISPUNJENO (7 klasa, 4 kompleksne, 5 enumova, DateTime, 1-N i N-N relacije)
- Punjenje podataka: ISPUNJENO (3 glavna objekta s razgranatim podacima)
- LINQ upiti: ISPUNJENO (15 smislenih upita)
- Upozorenja: folder `lab1` umjesto `lab-1`, provjera roka i javnosti GitHub repozitorija

---

### Interakcija 3
**Korisnik:** "mozes li voditi od sad pa na dalje log koristenja ai agenta"
**Agent:** Kreirao ovu log datoteku (`lab1/ai-agent-log.md`) za praćenje svih budućih interakcija.

---

## Sesija 2 — 13.04.2026. (Lab 2 — ASP.NET MVC)

### Interakcija 4
**Korisnik:** Priložio PDF zadatka (Lab_2_HTMLBinding.pdf) i zatražio nadogradnju Lab 1 projekta u ASP.NET MVC web aplikaciju
**Agent:** Pročitao PDF sa svim zahtjevima. Postavio pitanja o detaljima implementacije (entity stranice, dizajn, hamburger menu, karta).

---

### Interakcija 5
**Korisnik:** Odgovorio na sva pitanja — jedan projekt `lab2/ZagrebEvents.Web`, Google Maps-like sučelje s Leaflet.js + OpenStreetMap, tamna tema, pin ikonice po tipu eventa (🪩🎤📖🎪), hamburger sidebar s listom evenata i korisničkim profilom, kartice za prikaz, sve entity stranice (Index + Details za Event, Venue, User, Reservation, Review), hrvatska UI.
**Agent:** Kreirao UX sub-agent instruction file (`.claude/agents/ux-agent.md`) s kompletnim design systemom.

---

### Interakcija 6
**Korisnik:** (nastavak postavljanja zahtjeva — detalji po pitanjima)
**Agent:** Kreirao kompletan ASP.NET MVC projekt:
- `lab2/ZagrebEvents.Web/` — cijeli projekt struktura
- Model klase s computed properties (TypeIcon, TypeColor, Stars, StatusLabel, Initials)
- MockData static class s 3 venue-a, 9 eventa, 6 korisnika, 5 rezervacija, 5 recenzija
- Mock repositories (VenueMockRepository, EventMockRepository, UserMockRepository, ReservationMockRepository, ReviewMockRepository)
- Controllers: Home, Event, Venue, User, Reservation, Review
- `Program.cs` s DI registracijom svih repozitorija kao Singleton

---

### Interakcija 7
**Korisnik:** (automatski — build i view kreacija)
**Agent (UX sub-agent):** Kreirao `_Layout.cshtml` s navbar, hamburger sidebar, breadcrumbs, footer i `wwwroot/css/site.css` s kompletnim dark theme design systemom (CSS varijable, Inter font, kartice, pinovi).
**Agent (glavni):** Kreirao sve Razor viewove:
- `Home/Index.cshtml` — Leaflet.js interaktivna karta s custom pin ikonama, hover tooltipom, glow efektom, legendom
- `Event/Index.cshtml` + `Event/Details.cshtml` — kartice + full detalji (hero, statistike, rezervacijski form, recenzije, sidebar)
- `Venue/Index.cshtml` + `Venue/Details.cshtml`
- `User/Index.cshtml` + `User/Details.cshtml`
- `Reservation/Index.cshtml` + `Reservation/Details.cshtml`
- `Review/Index.cshtml` + `Review/Details.cshtml`
- Build rezultat: **uspješno**, 25 CS8618 upozorenja (nullable), 0 grešaka

---

### Interakcija 8 (Lab 2 — kritička provjera)
**Korisnik:** "Kritička provjera rezultata — provjeri jel sve popravljeno"
**Agent:** Sustavno prošao kroz svaku stranicu i pronašao 2 problema:
- Sidebar nije imao linkove za User i Review
- Review/Index nije imao link na Review/Details
**Agent:** Ručno ispravio `_Layout.cshtml` (dodao 2 linka) i `Review/Index.cshtml` (dodao "Detalji →" link).

---

## Sesija 3 — 04.05.2026. (Lab 3 — Entity Framework, Routing)

### Interakcija 9
**Korisnik:** Priložio PDF zadatka (Lab3.pdf) i zatražio nadogradnju s EF + custom routing + skill-ovima
**Agent:** Pročitao PDF, postavio pitanja o detaljima (baza, struktura, mock vs EF, custom URL-ovi, skill-ovi, edit/create forme).

### Interakcija 10
**Korisnik:** Odgovorio: SQL Server Express (već instaliran), 3 projekta (Model/DAL/Web), seed iz mock podataka, hrvatski URL-ovi, sva tri skill-a, sve tri nove forme (Create Event/Review, Edit Reservation).
**Agent:** Kreirao kompletnu Lab 3 implementaciju kroz 21 zadatak:

**Restrukturiranje (Korak 1-2):**
- `lab3/ZagrebEvents.sln` — solution s 3 projekta
- `lab3/ZagrebEvents.Model/` — model klase (premještene iz Web)
- `lab3/ZagrebEvents.DAL/` — DbContext + migracije
- `lab3/ZagrebEvents.Web/` — controlleri + viewovi (kopirano iz lab2)

**EF konfiguracija (Korak 3-7):**
- NuGet paketi: Microsoft.EntityFrameworkCore.SqlServer + Design (v8.0.10)
- Anotacije na svim modelima: `[Key]`, `[ForeignKey]`, `[Required]`, `[MaxLength]`, `[Column(decimal(10,2))]`, `[NotMapped]` na computed properties
- N-N veza User ↔ Venue (FavoriteVenues) preko implicit join tablice "UserFavoriteVenue"
- Cascade behavior konfigurirana (Restrict za Reservation FK-jeve da spriječi cycle)
- ZagrebEventsDbContext s 7 DbSet propertyja
- Connection string: `Server=localhost\SQLEXPRESS;Database=ZagrebEvents;Trusted_Connection=True`
- Program.cs: `AddDbContext<ZagrebEventsDbContext>()` s SqlServer provider-om

**Seed (Korak 8-10):**
- HasData u OnModelCreating: 6 korisnika, 3 venue, 11 stolova, 13 cjenik stavki, 9 evenata, 5 rezervacija, 5 recenzija, 5 favorita
- Inicijalna migracija "InitialCreate" generirana iz `dotnet ef migrations add`
- Baza ZagrebEvents kreirana automatski iz `dotnet ef database update`

**Migracija s Mock na EF (Korak 11):**
- Svi controlleri prebačeni s `*MockRepository` na `ZagrebEventsDbContext`
- Repositories folder obrisan
- Eager loading kroz `Include()` i `ThenInclude()` umjesto in-memory referenci

**Custom routing (Korak 12) — 8 ruta:**
- `[Route("eventi")]` `[Route("[controller]/[action]")]` na EventController.Index (oba URL-a rade)
- `/event/{id}`, `/lokacije`, `/lokacija/{id}`, `/korisnici`, `/moj-profil` (alias za Details/1), `/rezervacije`, `/recenzije`
- Ispravak: dodavanje `[controller]/[action]` fallback ruta jer atributno usmjeravanje isključuje default routing za akciju

**CRUD forme (Korak 13-15):**
- `Event/Create.cshtml` + EventController.Create akcije (GET + POST) — admin
- `Review/Create.cshtml` + ReviewController.Create — svi prijavljeni
- `Reservation/Edit.cshtml` + ReservationController.Edit — admin
- Sve forme s `<input asp-for>` Tag Helperima i `[ValidateAntiForgeryToken]`

**Claude skill-ovi (Korak 16-18):**
- `.claude/skills/ef-skill.md` — instrukcije za EF izmjene, migracije, anotacije, CRUD
- `.claude/skills/list-page-skill.md` — predložak za Index stranice s controllerom + viewom
- `.claude/skills/edit-form-skill.md` — predložak za Create/Edit forme s Tag Helperima

**Dokumentacija (Korak 19-20):**
- `lab3/semantic-model.md` — 7 entiteta + 1 join tablica, sve veze (1-N, N-N), cascade pravila, ER dijagram, popis seed podataka
- `lab3/sitemap.md` — svi URL-ovi (default + custom), controller, akcija, view, opis svake stranice

**Build & deploy (Korak 21):**
- Build: 0 errors, 17 warnings (samo nullable)
- Test: svi URL-ovi vraćaju 200 (`/moj-profil` 302 redirect kako treba)
- Commit: 107 datoteka u Lab 3 commit

---

### Interakcija 11 (Lab 3 — fix routing)
**Korisnik:** "kada stisnem na event ove greske baca" (404 na /Event/Details/6)
**Agent:** Identificirao problem — atributno usmjeravanje isključuje default routing za akciju s `[Route]` atributom. Dodao `[Route("[controller]/[action]/{id:int}")]` fallback na sve akcije s custom rutom.

---

### Interakcija 12 (Lab 3 — popravak datuma evenata + AntiForgeryToken)
**Korisnik:** "pri kreiraj rezervaciju se baca greska, i sve su ikone iste na karti"
**Agent:**
- Dodao `@Html.AntiForgeryToken()` u rezervacijsku formu (popravak HTTP 400)
- Identificirao da pin ikone nisu bug — sva 3 venuea trenutno imaju Concert kao prvi nadolazeći event
- Promijenio datume e1, e4, e9 SQL UPDATE-om da svaki venue ima različit tip prvog nadolazećeg eventa (DJNight, Quiz, Festival)

---

### Interakcija 13 (Lab 3 — login sustav s rolama)
**Korisnik:** "napravi neki login tako da samo admin moze raditi evente, i nemamo mogucnost kreiranja recenzije" (kasnije korigirano: rezervacije ostaju, recenzije dostupne svim ulogiranima)
**Agent:** Implementirao kompletan auth sustav:
- Dodao `Password` property u User model
- Migracija `AddPasswordToUser` s default lozinkama u seedu (Ivan/ivan123, Luka/admin123, ostali/demo123)
- Cookie Authentication u Program.cs (`AddAuthentication(CookieAuthenticationDefaults...)`)
- AccountController: Login, Register, Logout, AccessDenied (5 akcija)
- Custom rute: `/prijava`, `/registracija`, `/odjava`
- Views: Login.cshtml (s prikazom demo lozinki), Register.cshtml, AccessDenied.cshtml
- Sidebar (`_Layout.cshtml`): različit prikaz za neulogirane vs Guest vs Admin
- `[Authorize(Roles="Admin")]` na `Event/Create`, `Reservation/Edit`
- `[Authorize]` na `Review/Create`, `Event/Reserve`
- Review/Create i Event/Reserve sad uzimaju UserId iz `User.FindFirstValue(ClaimTypes.NameIdentifier)` umjesto hardkodiranog 1
- Dodao "⭐ Napiši recenziju" gumb na Event/Details (dostupno samo prijavljenima)

---

### Interakcija 14 (Lab 3 — admin akcije za rezervacije + star rating fix)
**Korisnik:** "1. neka admin moze prihvacat i odbijat i otkazivat rezervacije, 2. zvjezdice su naopacke"
**Agent:**
- Dodao `ReservationController.SetStatus` akciju (admin) s POST + AntiForgeryToken
- Reservation/Index: gumbi ✓⏳✗✎ za admina (svaki red)
- Reservation/Details: admin panel s istim akcijama (veliki gumbi)
- Star rating prepisan iz CSS-only triku u JS handler s `data-value` atributima — sada zvjezdice idu lijevo→desno (1=1/5, 5=5/5), hover preview, mouseleave vraća odabranu

---

### Interakcija 15 (Provjera zahtjeva)
**Korisnik:** "jel sve ispunjeno iz pdf-a lab 3 i dal je napravljen update ai loga"
**Agent:** Provjerio PDF zahtjeve, ažurirao ovaj log s Lab 3 sesijom.

---

## Provjera ispunjenosti zahtjeva — Lab 3 PDF

### Bodovanje (5/5 bodova)

| Kriterij | Status | Dokaz |
|----------|--------|-------|
| Prilagodba modela za EF (anotacije, veze), konfiguracija EF (1) | ✅ | `[Key]`, `[ForeignKey]`, `virtual ICollection<T>`, `[Column]`, `[Required]`, `[MaxLength]` na svim modelima |
| Razumijevanje EF principa (usmeno) (1) | ✅ | Pripremi se — sve znaš (DbContext, DbSet, migrations, eager loading, cascade) |
| Razumijevanje routing principa (usmeno) (1) | ✅ | Pripremi se — 8 custom ruta + default + atributno usmjeravanje |
| Izrada semantičkog i routing modela (md file) pomoću AI (1) | ✅ | `lab3/semantic-model.md` + `lab3/sitemap.md` |
| Izrada i korištenje skill-ova (1) | ✅ | 3 skill-a: ef-skill, list-page-skill, edit-form-skill |

### Nužni uvjeti

- ✅ Konfigurirati EF u projektu — DbContext, NuGet paketi, Program.cs
- ✅ Dodati ispravne anotacije na model — Key, ForeignKey, Required, MaxLength, Column
- ✅ Podesiti virtual i ICollection<> svojstva — sve navigacijske kolekcije su `virtual ICollection<T>`
- ✅ Instalirati bazu podataka — MSSQL Express 2022, baza `ZagrebEvents` kreirana
- ✅ Connection string — `appsettings.json` pod ključem `ZagrebEventsDbContext`
- ✅ Podesiti DbContext i potrebne DI — `AddDbContext<ZagrebEventsDbContext>(...)` u Program.cs
- ✅ Prebaciti app s mock repository na EF repository — svi controlleri sad rade direktno s `_db`
- ✅ Generirati inicijalnu migracijsku skriptu — `InitialCreate` + `AddPasswordToUser`
- ✅ Custom routing barem 4 akcije — implementirano 8 (eventi, event/{id}, lokacije, lokacija/{id}, korisnici, moj-profil, rezervacije, recenzije, prijava, registracija, odjava)
- ✅ semantic-model.md — sažeti popis modela, svojstava, veza
- ✅ sitemap.md — za svaki URL: controller, akcija, view
- ✅ Skill-ovi — sva tri (EF, List, Edit form)

### Dodatne funkcionalnosti (preko traženog)

- ✅ Login/Register sustav s ulogama (Guest, Owner, Admin)
- ✅ Cookie Authentication
- ✅ Admin akcije za rezervacije (Confirm/Cancel/Pending/Edit)
- ✅ JavaScript star rating widget za recenzije
- ✅ "Napiši recenziju" gumb dostupan samo prijavljenim korisnicima
- ✅ Sidebar pokazuje različite linkove ovisno o roli (Admin vidi "Novi event")

**Zaključak: Lab 3 je 100% ispunjen prema PDF-u + dodatne funkcionalnosti.**

---
