# Lab 5 — Pregled koda (API, Auth, Tests)

Vodič kroz sve dijelove koda koje Lab 5 zahtijeva + dodatne funkcionalnosti.
Svaka stavka navodi **datoteku i klasu/metodu** gdje se nalazi.

> Putanje su relativne na `lab5/`. Projekti: `ZagrebEvents.Model` (entiteti),
> `ZagrebEvents.DAL` (DbContext, migracije, seed), `ZagrebEvents.Web` (MVC + API),
> `ZagrebEvents.Tests` (integracijski testovi).

---

## 1. API podrška za sve entitete (CRUD, DTO) — 2 boda

### API controlleri — `ZagrebEvents.Web/Controllers/Api/`
Svaki je označen `[ApiController]`, nasljeđuje `ControllerBase` i ima attribute routing:

| Controller | Ruta | CRUD |
|---|---|---|
| `EventApiController` | `api/events` | GET all (+`?q=` pretraga), GET by id, POST, PUT, DELETE (soft) |
| `VenueApiController` | `api/venues` | isto |
| `ReservationApiController` | `api/reservations` | isto |
| `ReviewApiController` | `api/reviews` | isto |
| `TableApiController` | `api/tables` | isto |
| `PriceListItemApiController` | `api/pricelistitems` | isto |
| `UserApiController` | `api/users` | GET all/by id, DELETE (POST/PUT namjerno nema — korisnici nastaju registracijom, *"gdje poslovna pravila to dopuštaju"*) |

- **Pretraga**: svaki GET all prima `string? q` query parametar (`Where(x => x.Name.Contains(q))…`)
- **HTTP statusi**: `Ok()` 200, `CreatedAtAction()` 201, `BadRequest(ModelState)` 400, `NotFound()` 404, `NoContent()` 204
- **Zaštita**: POST/PUT/DELETE imaju `[Authorize]`/role; GET su javni
- `ApiController.cs` (isti folder) — pomoćni MVC controller za AJAX autocomplete (`/Api/SearchVenues`), nije dio REST API-ja

### DTO klase — `ZagrebEvents.Web/Dtos/Dtos.cs`
16 klasa: `EventDto`, `EventCreateDto`, `EventSummaryDto`, `VenueDto`, `VenueCreateDto`,
`VenueSummaryDto`, `ReservationDto/CreateDto`, `ReviewDto/CreateDto`, `TableDto/CreateDto`,
`PriceListItemDto/CreateDto`, `UserDto`, `UserSummaryDto`.

- **Ugniježđeni DTO-ovi**: `EventDto.Venue → VenueSummaryDto`, `ReservationDto.User → UserSummaryDto`
  i `.Event → EventSummaryDto`, `ReviewDto.User/Event` — povezani podaci bez cikličkog JSON-a
- Interna polja (`DeletedAt`, navigacijske kolekcije, `OwnerAppUserId`) **nisu** izložena
- Mapiranje: privatne `ToDto(...)` metode u svakom API controlleru (ručno, bez AutoMappera)

---

## 2. Autentikacija (ASP.NET Core Identity) — 1 bod

### AppUser proširen s OIB i JMBG — `ZagrebEvents.Model/AppUser.cs`
```csharp
public class AppUser : IdentityUser
{
    [StringLength(11, MinimumLength = 11)] [RegularExpression("^[0-9]*$")]
    public string OIB { get; set; }
    [StringLength(13, MinimumLength = 13)] [RegularExpression("^[0-9]*$")]
    public string JMBG { get; set; }
}
```

### DbContext nasljeđuje IdentityDbContext — `ZagrebEvents.DAL/ZagrebEventsDbContext.cs`
```csharp
public class ZagrebEventsDbContext : IdentityDbContext<AppUser, IdentityRole, string>
```
Identity tablice (AspNetUsers, AspNetRoles…) žive u istoj bazi kao domenski entiteti.

### Konfiguracija — `ZagrebEvents.Web/Program.cs`
- `AddIdentity<AppUser, IdentityRole>()` + `.AddEntityFrameworkStores<ZagrebEventsDbContext>()`
- `app.UseAuthentication()` **prije** `app.UseAuthorization()`
- Pri startu poziva `IdentitySeeder.SeedAsync(...)` (oko linije 100)

### Lokalna registracija i prijava — `ZagrebEvents.Web/Controllers/AccountController.cs`
Vlastiti MVC controller (umjesto scaffoldanih Razor Pages):
`Login` (GET/POST, `SignInManager.PasswordSignInAsync`), `Register` (GET/POST,
`UserManager.CreateAsync` — sprema i OIB/JMBG, kreira domenski `User` profil),
`Logout`, `AccessDenied`. Viewovi: `Views/Account/Login.cshtml`, `Register.cshtml`.

### Arhitektura korisnika ("Opcija B")
- `AppUser` (Identity, string GUID id) = autentikacija
- domenski `User` (`ZagrebEvents.Model/User.cs`, int id) = profil, rezervacije, recenzije
- veza: `User.AppUserId`; helper `ClaimsPrincipalExtensions.GetDomainUserId()`

### Seed rola i korisnika — `ZagrebEvents.DAL/IdentitySeeder.cs`
Kreira role **Admin, Owner, Guest** (`RoleManager.CreateAsync`), demo Identity račune i
povezuje ih s domenskim `User` zapisima (`AppUserId`).

---

## 3. Autorizacija — (dio 1. boda)

### Role + pravila po akcijama
- **Javno (anonimno)**: karta (`HomeController.Index`), liste i detalji evenata/lokacija, GET API
- **Prijavljeni**: rezervacije (`EventController.Reserve` ima `[Authorize]`), recenzije, profil
- **Admin ili Owner**: `[Authorize(Roles = "Admin,Owner")]` na Create/Edit/Delete u
  `EventController`, `VenueController` (Edit), `TableController`, `PriceListItemController`
- **Samo Admin**: brisanje lokacija/korisnika, `UserController.Index` (popis korisnika),
  `UserController.SetRole` (dodjela uloga)

### Resource-based autorizacija (Owner smije samo SVOJ venue)
`ZagrebEvents.Web/Services/ClaimsPrincipalExtensions.cs`:
```csharp
public static bool CanManageVenue(this ClaimsPrincipal user, string? ownerAppUserId)
// Admin -> true; Owner -> true samo ako je on vlasnik tog venuea
```
Koristi se u `VenueController.Edit/EditPost`, `EventController` (preko `CanManageVenueId`),
`TableController`, `PriceListItemController`, `ReservationController.SetStatus`.
Vlasništvo: `Venue.OwnerAppUserId` (`ZagrebEvents.Model/Venue.cs`).

### Admin dodjela uloga — `ZagrebEvents.Web/Controllers/UserController.cs`
Metoda `SetRole(int id, string role, int? venueId)`: mijenja Identity rolu
(`UserManager.RemoveFromRolesAsync` + `AddToRoleAsync`), domensku `User.Role` i
`Venue.OwnerAppUserId`. UI: panel na `Views/User/Details.cshtml` (samo Admin).

### Claims — `ZagrebEvents.Web/Services/AppUserClaimsPrincipalFactory.cs`
U cookie dodaje claim s domenskim UserId-em (da se ne ide u bazu na svaki zahtjev).

---

## 4. Upload datoteka (Dropzone) — 1 bod

### Model — `ZagrebEvents.Model/Attachment.cs`
`Id, EventId, FileName, FilePath, ContentType, FileSize, CreatedAt` — metapodaci u bazi,
datoteka na disku. Kolekcija `Event.Attachments`.

### Server akcije — `ZagrebEvents.Web/Controllers/EventController.cs`
- `UploadAttachment(int eventId, IFormFile file)` — POST, prima multipart s Dropzonea;
  **validira** veličinu (max 5 MB) i ekstenziju (jpg/png/gif/webp/pdf); sprema u
  `wwwroot/uploads/events/{eventId}/{guid}{ext}`; metapodatke u `Attachments` tablicu
- `GetAttachments(int eventId)` — vraća partial `_AttachmentList` (AJAX učitavanje popisa)
- `DeleteAttachment(int id)` — briše datoteku s diska + zapis iz baze (AJAX)

### Klijent — `ZagrebEvents.Web/Views/Event/Edit.cshtml`
Dropzone forma (`class="dropzone"`, `asp-action="UploadAttachment"`), `success` callback
poziva `loadAttachments()` koji `$("#attachmentList").load(...)` povlači svjež popis.
Partial: `Views/Event/_AttachmentList.cshtml`. Upload je na **Edit** formi (event već ima ID).

---

## 5. Google OAuth (3rd party login) — 1 bod

### Konfiguracija — `ZagrebEvents.Web/Program.cs` (oko linije 76)
```csharp
.AddAuthentication().AddGoogle(options => {
    options.ClientId  = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});
```
**Tajne su u user-secrets** (`dotnet user-secrets set ...`) — nisu u kodu ni u gitu. ✔

### Flow — `ZagrebEvents.Web/Controllers/AccountController.cs`
- `ExternalLogin` — Challenge prema Googleu
- `ExternalLoginCallback` — povratak s authorization codeom; postojeći korisnik se prijavi
- `ExternalLoginConfirmation` — prva prijava: forma za **OIB/JMBG** (obavezno i za OAuth
  korisnike), kreira `AppUser` + domenski `User` + `AddLoginAsync` (vanjski login)
- View: `Views/Account/ExternalLoginConfirmation.cshtml`; gumb "Prijava Googleom" na Login stranici

---

## 6. Integracijski testovi API-ja — 2 boda

### Infrastruktura — `ZagrebEvents.Tests/`
- `CustomWebApplicationFactory.cs` — `WebApplicationFactory<Program>`: zamjenjuje SQL Server
  s **InMemory** bazom (`UseInMemoryDatabase`, jedinstveno ime po testu → izolacija),
  postavlja test autentikaciju kao default shemu, seeda minimalne podatke
- `TestAuthHandler.cs` — lažni auth handler: zahtjev s headerom dobiva identitet i rolu
  (npr. Admin) bez pravog logina; bez headera → 401
- `GlobalUsings.cs` — zajednički usingi

### Testovi — `EventApiTests.cs`, `VenueApiTests.cs` (18 testova, svi prolaze)
Obrazac **Arrange-Act-Assert** kroz pravi HTTP (`HttpClient` iz factoryja):
- `GetAll_ReturnsOkAndCollection` — 200 + kolekcija
- `GetById_ReturnsEvent_WhenExists` / `GetById_Returns404_WhenNotExists`
- `Create_Returns201_WhenValid` / `Create_Returns400_WhenInvalid` (validacija)
  / `Create_Returns401_WhenNotAuthenticated` (autorizacija)
- `Update_ReturnsOk_WhenExists` / `Update_Returns404_WhenNotExists`
- `Delete_ReturnsNoContent_WhenExists` / `Delete_Returns404_WhenNotExists`

Pokretanje: `dotnet test` iz `lab5/`.

---

## 7. Dodatne funkcionalnosti (izvan zahtjeva laba)

| Funkcionalnost | Gdje |
|---|---|
| **Interaktivna karta** (Leaflet self-hosted, clustering, filter po tipu, welcome popup 1×/po startu app, geolokacija 📍, Google Maps/Waze upute) | `Views/Home/Index.cshtml`, `HomeController.cs` (`_welcomeShown`), `wwwroot/lib/leaflet*` |
| **Logo venuea u pinu** + obrub = tip eventa + ⭐ featured spotlight | `Views/Home/Index.cshtml` (markerHtml), `Venue.LogoUrl/Initials`, `Event.IsFeatured`, `wwwroot/img/logos/` |
| **Tlocrt + zauzetost stolova** (modal, markeri slobodan/zauzet preko slike) | `Venue.FloorPlanUrl`, `Table.PosX/PosY`, `Views/Event/Details.cshtml` (floorplan modal), H2O tlocrt: `wwwroot/img/floorplans/h2o-tlocrt.svg` |
| **Zaštita od duple rezervacije** (zauzeti stolovi se ne nude + server-side guard) | `Views/Event/Details.cshtml` (`freeTables`), `EventController.Reserve` |
| **Email potvrde rezervacija** (zaprimljena / potvrđena / odbijena; SMTP iz user-secrets, fallback u `App_Data/emails/`) | `Services/EmailService.cs`, pozivi u `EventController.Reserve` i `ReservationController.SetStatus` |
| **AJAX pretraga lista** (debounce, šalje sva polja forme) | `wwwroot/js/site.js` (`initListSearch`), `*Controller.SearchPartial` + `_*ListPartial` viewovi |
| **Instagram gumb na venue** | `Venue.InstagramUrl`, `Views/Venue/Details.cshtml` (`.ig-btn`) |
| **Sve recenzije venuea + prosjek** | `Views/Venue/Details.cshtml` (`venueReviews`), `VenueController.Details` (Include User) |
| **Soft delete svugdje** | `DeletedAt` na entitetima; upiti filtriraju `DeletedAt == null` |
| **Seed podaci** (24+ stvarne zagrebačke lokacije, ~40 evenata, ~550 stolova — klubovi 30, festivali samo VIP) | `ZagrebEvents.DAL/ZagrebEventsDbContext.cs` (`OnModelCreating` HasData + generirani stolovi) |

---

## Brzi vodič za demonstraciju
1. **API**: `GET http://localhost:5053/api/events` (javno) → JSON s ugniježđenim VenueSummaryDto
2. **401**: `POST /api/events` bez prijave → 401/redirect
3. **Identity**: registracija s OIB/JMBG → prijava → uloge (admin: luka.peric@admin.com)
4. **Google login**: gumb na Login stranici (ClientId/Secret u user-secrets — pokazati `dotnet user-secrets list`)
5. **Upload**: Event → Uredi → povuci sliku u Dropzone → popis se osvježi AJAX-om → Obriši
6. **Testovi**: `dotnet test` → 18/18
